/*!
 * @overview es6-promise - a tiny implementation of Promises/A+.
 * @copyright Copyright (c) 2014 Yehuda Katz, Tom Dale, Stefan Penner and contributors (Conversion to ES6 API by Jake Archibald)
 * @license   Licensed under MIT license
 *            See https://raw.githubusercontent.com/stefanpenner/es6-promise/master/LICENSE
 * @version   4.0.5
 */
(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.ES6Promise = factory());
}(this, (function () {
    'use strict';
    function objectOrFunction(x) {
        return typeof x === 'function' || typeof x === 'object' && x !== null;
    }
    function isFunction(x) {
        return typeof x === 'function';
    }
    var _isArray = undefined;
    if (!Array.isArray) {
        _isArray = function (x) {
            return Object.prototype.toString.call(x) === '[object Array]';
        };
    }
    else {
        _isArray = Array.isArray;
    }
    var isArray = _isArray;
    var len = 0;
    var vertxNext = undefined;
    var customSchedulerFn = undefined;
    var asap = function asap(callback, arg) {
        queue[len] = callback;
        queue[len + 1] = arg;
        len += 2;
        if (len === 2) {
            if (customSchedulerFn) {
                customSchedulerFn(flush);
            }
            else {
                scheduleFlush();
            }
        }
    };
    function setScheduler(scheduleFn) {
        customSchedulerFn = scheduleFn;
    }
    function setAsap(asapFn) {
        asap = asapFn;
    }
    var browserWindow = typeof window !== 'undefined' ? window : undefined;
    var browserGlobal = browserWindow || {};
    var BrowserMutationObserver = browserGlobal.MutationObserver || browserGlobal.WebKitMutationObserver;
    var isNode = typeof self === 'undefined' && typeof process !== 'undefined' && ({}).toString.call(process) === '[object process]';
    var isWorker = typeof Uint8ClampedArray !== 'undefined' && typeof importScripts !== 'undefined' && typeof MessageChannel !== 'undefined';
    function useNextTick() {
        return function () {
            return process.nextTick(flush);
        };
    }
    function useVertxTimer() {
        if (typeof vertxNext !== 'undefined') {
            return function () {
                vertxNext(flush);
            };
        }
        return useSetTimeout();
    }
    function useMutationObserver() {
        var iterations = 0;
        var observer = new BrowserMutationObserver(flush);
        var node = document.createTextNode('');
        observer.observe(node, { characterData: true });
        return function () {
            node.data = iterations = ++iterations % 2;
        };
    }
    function useMessageChannel() {
        var channel = new MessageChannel();
        channel.port1.onmessage = flush;
        return function () {
            return channel.port2.postMessage(0);
        };
    }
    function useSetTimeout() {
        var globalSetTimeout = setTimeout;
        return function () {
            return globalSetTimeout(flush, 1);
        };
    }
    var queue = new Array(1000);
    function flush() {
        for (var i = 0; i < len; i += 2) {
            var callback = queue[i];
            var arg = queue[i + 1];
            callback(arg);
            queue[i] = undefined;
            queue[i + 1] = undefined;
        }
        len = 0;
    }
    function attemptVertx() {
        try {
            var r = require;
            var vertx = r('vertx');
            vertxNext = vertx.runOnLoop || vertx.runOnContext;
            return useVertxTimer();
        }
        catch (e) {
            return useSetTimeout();
        }
    }
    var scheduleFlush = undefined;
    if (isNode) {
        scheduleFlush = useNextTick();
    }
    else if (BrowserMutationObserver) {
        scheduleFlush = useMutationObserver();
    }
    else if (isWorker) {
        scheduleFlush = useMessageChannel();
    }
    else if (browserWindow === undefined && typeof require === 'function') {
        scheduleFlush = attemptVertx();
    }
    else {
        scheduleFlush = useSetTimeout();
    }
    function then(onFulfillment, onRejection) {
        var _arguments = arguments;
        var parent = this;
        var child = new this.constructor(noop);
        if (child[PROMISE_ID] === undefined) {
            makePromise(child);
        }
        var _state = parent._state;
        if (_state) {
            (function () {
                var callback = _arguments[_state - 1];
                asap(function () {
                    return invokeCallback(_state, child, callback, parent._result);
                });
            })();
        }
        else {
            subscribe(parent, child, onFulfillment, onRejection);
        }
        return child;
    }
    function resolve(object) {
        var Constructor = this;
        if (object && typeof object === 'object' && object.constructor === Constructor) {
            return object;
        }
        var promise = new Constructor(noop);
        _resolve(promise, object);
        return promise;
    }
    var PROMISE_ID = Math.random().toString(36).substring(16);
    function noop() { }
    var PENDING = void 0;
    var FULFILLED = 1;
    var REJECTED = 2;
    var GET_THEN_ERROR = new ErrorObject();
    function selfFulfillment() {
        return new TypeError("You cannot resolve a promise with itself");
    }
    function cannotReturnOwn() {
        return new TypeError('A promises callback cannot return that same promise.');
    }
    function getThen(promise) {
        try {
            return promise.then;
        }
        catch (error) {
            GET_THEN_ERROR.error = error;
            return GET_THEN_ERROR;
        }
    }
    function tryThen(then, value, fulfillmentHandler, rejectionHandler) {
        try {
            then.call(value, fulfillmentHandler, rejectionHandler);
        }
        catch (e) {
            return e;
        }
    }
    function handleForeignThenable(promise, thenable, then) {
        asap(function (promise) {
            var sealed = false;
            var error = tryThen(then, thenable, function (value) {
                if (sealed) {
                    return;
                }
                sealed = true;
                if (thenable !== value) {
                    _resolve(promise, value);
                }
                else {
                    fulfill(promise, value);
                }
            }, function (reason) {
                if (sealed) {
                    return;
                }
                sealed = true;
                _reject(promise, reason);
            }, 'Settle: ' + (promise._label || ' unknown promise'));
            if (!sealed && error) {
                sealed = true;
                _reject(promise, error);
            }
        }, promise);
    }
    function handleOwnThenable(promise, thenable) {
        if (thenable._state === FULFILLED) {
            fulfill(promise, thenable._result);
        }
        else if (thenable._state === REJECTED) {
            _reject(promise, thenable._result);
        }
        else {
            subscribe(thenable, undefined, function (value) {
                return _resolve(promise, value);
            }, function (reason) {
                return _reject(promise, reason);
            });
        }
    }
    function handleMaybeThenable(promise, maybeThenable, then$$) {
        if (maybeThenable.constructor === promise.constructor && then$$ === then && maybeThenable.constructor.resolve === resolve) {
            handleOwnThenable(promise, maybeThenable);
        }
        else {
            if (then$$ === GET_THEN_ERROR) {
                _reject(promise, GET_THEN_ERROR.error);
            }
            else if (then$$ === undefined) {
                fulfill(promise, maybeThenable);
            }
            else if (isFunction(then$$)) {
                handleForeignThenable(promise, maybeThenable, then$$);
            }
            else {
                fulfill(promise, maybeThenable);
            }
        }
    }
    function _resolve(promise, value) {
        if (promise === value) {
            _reject(promise, selfFulfillment());
        }
        else if (objectOrFunction(value)) {
            handleMaybeThenable(promise, value, getThen(value));
        }
        else {
            fulfill(promise, value);
        }
    }
    function publishRejection(promise) {
        if (promise._onerror) {
            promise._onerror(promise._result);
        }
        publish(promise);
    }
    function fulfill(promise, value) {
        if (promise._state !== PENDING) {
            return;
        }
        promise._result = value;
        promise._state = FULFILLED;
        if (promise._subscribers.length !== 0) {
            asap(publish, promise);
        }
    }
    function _reject(promise, reason) {
        if (promise._state !== PENDING) {
            return;
        }
        promise._state = REJECTED;
        promise._result = reason;
        asap(publishRejection, promise);
    }
    function subscribe(parent, child, onFulfillment, onRejection) {
        var _subscribers = parent._subscribers;
        var length = _subscribers.length;
        parent._onerror = null;
        _subscribers[length] = child;
        _subscribers[length + FULFILLED] = onFulfillment;
        _subscribers[length + REJECTED] = onRejection;
        if (length === 0 && parent._state) {
            asap(publish, parent);
        }
    }
    function publish(promise) {
        var subscribers = promise._subscribers;
        var settled = promise._state;
        if (subscribers.length === 0) {
            return;
        }
        var child = undefined, callback = undefined, detail = promise._result;
        for (var i = 0; i < subscribers.length; i += 3) {
            child = subscribers[i];
            callback = subscribers[i + settled];
            if (child) {
                invokeCallback(settled, child, callback, detail);
            }
            else {
                callback(detail);
            }
        }
        promise._subscribers.length = 0;
    }
    function ErrorObject() {
        this.error = null;
    }
    var TRY_CATCH_ERROR = new ErrorObject();
    function tryCatch(callback, detail) {
        try {
            return callback(detail);
        }
        catch (e) {
            TRY_CATCH_ERROR.error = e;
            return TRY_CATCH_ERROR;
        }
    }
    function invokeCallback(settled, promise, callback, detail) {
        var hasCallback = isFunction(callback), value = undefined, error = undefined, succeeded = undefined, failed = undefined;
        if (hasCallback) {
            value = tryCatch(callback, detail);
            if (value === TRY_CATCH_ERROR) {
                failed = true;
                error = value.error;
                value = null;
            }
            else {
                succeeded = true;
            }
            if (promise === value) {
                _reject(promise, cannotReturnOwn());
                return;
            }
        }
        else {
            value = detail;
            succeeded = true;
        }
        if (promise._state !== PENDING) {
        }
        else if (hasCallback && succeeded) {
            _resolve(promise, value);
        }
        else if (failed) {
            _reject(promise, error);
        }
        else if (settled === FULFILLED) {
            fulfill(promise, value);
        }
        else if (settled === REJECTED) {
            _reject(promise, value);
        }
    }
    function initializePromise(promise, resolver) {
        try {
            resolver(function resolvePromise(value) {
                _resolve(promise, value);
            }, function rejectPromise(reason) {
                _reject(promise, reason);
            });
        }
        catch (e) {
            _reject(promise, e);
        }
    }
    var id = 0;
    function nextId() {
        return id++;
    }
    function makePromise(promise) {
        promise[PROMISE_ID] = id++;
        promise._state = undefined;
        promise._result = undefined;
        promise._subscribers = [];
    }
    function Enumerator(Constructor, input) {
        this._instanceConstructor = Constructor;
        this.promise = new Constructor(noop);
        if (!this.promise[PROMISE_ID]) {
            makePromise(this.promise);
        }
        if (isArray(input)) {
            this._input = input;
            this.length = input.length;
            this._remaining = input.length;
            this._result = new Array(this.length);
            if (this.length === 0) {
                fulfill(this.promise, this._result);
            }
            else {
                this.length = this.length || 0;
                this._enumerate();
                if (this._remaining === 0) {
                    fulfill(this.promise, this._result);
                }
            }
        }
        else {
            _reject(this.promise, validationError());
        }
    }
    function validationError() {
        return new Error('Array Methods must be provided an Array');
    }
    ;
    Enumerator.prototype._enumerate = function () {
        var length = this.length;
        var _input = this._input;
        for (var i = 0; this._state === PENDING && i < length; i++) {
            this._eachEntry(_input[i], i);
        }
    };
    Enumerator.prototype._eachEntry = function (entry, i) {
        var c = this._instanceConstructor;
        var resolve$$ = c.resolve;
        if (resolve$$ === resolve) {
            var _then = getThen(entry);
            if (_then === then && entry._state !== PENDING) {
                this._settledAt(entry._state, i, entry._result);
            }
            else if (typeof _then !== 'function') {
                this._remaining--;
                this._result[i] = entry;
            }
            else if (c === Promise) {
                var promise = new c(noop);
                handleMaybeThenable(promise, entry, _then);
                this._willSettleAt(promise, i);
            }
            else {
                this._willSettleAt(new c(function (resolve$$) {
                    return resolve$$(entry);
                }), i);
            }
        }
        else {
            this._willSettleAt(resolve$$(entry), i);
        }
    };
    Enumerator.prototype._settledAt = function (state, i, value) {
        var promise = this.promise;
        if (promise._state === PENDING) {
            this._remaining--;
            if (state === REJECTED) {
                _reject(promise, value);
            }
            else {
                this._result[i] = value;
            }
        }
        if (this._remaining === 0) {
            fulfill(promise, this._result);
        }
    };
    Enumerator.prototype._willSettleAt = function (promise, i) {
        var enumerator = this;
        subscribe(promise, undefined, function (value) {
            return enumerator._settledAt(FULFILLED, i, value);
        }, function (reason) {
            return enumerator._settledAt(REJECTED, i, reason);
        });
    };
    function all(entries) {
        return new Enumerator(this, entries).promise;
    }
    function race(entries) {
        var Constructor = this;
        if (!isArray(entries)) {
            return new Constructor(function (_, reject) {
                return reject(new TypeError('You must pass an array to race.'));
            });
        }
        else {
            return new Constructor(function (resolve, reject) {
                var length = entries.length;
                for (var i = 0; i < length; i++) {
                    Constructor.resolve(entries[i]).then(resolve, reject);
                }
            });
        }
    }
    function reject(reason) {
        var Constructor = this;
        var promise = new Constructor(noop);
        _reject(promise, reason);
        return promise;
    }
    function needsResolver() {
        throw new TypeError('You must pass a resolver function as the first argument to the promise constructor');
    }
    function needsNew() {
        throw new TypeError("Failed to construct 'Promise': Please use the 'new' operator, this object constructor cannot be called as a function.");
    }
    function Promise(resolver) {
        this[PROMISE_ID] = nextId();
        this._result = this._state = undefined;
        this._subscribers = [];
        if (noop !== resolver) {
            typeof resolver !== 'function' && needsResolver();
            this instanceof Promise ? initializePromise(this, resolver) : needsNew();
        }
    }
    Promise.all = all;
    Promise.race = race;
    Promise.resolve = resolve;
    Promise.reject = reject;
    Promise._setScheduler = setScheduler;
    Promise._setAsap = setAsap;
    Promise._asap = asap;
    Promise.prototype = {
        constructor: Promise,
        then: then,
        'catch': function _catch(onRejection) {
            return this.then(null, onRejection);
        }
    };
    function polyfill() {
        var local = undefined;
        if (typeof global !== 'undefined') {
            local = global;
        }
        else if (typeof self !== 'undefined') {
            local = self;
        }
        else {
            try {
                local = Function('return this')();
            }
            catch (e) {
                throw new Error('polyfill failed because global object is unavailable in this environment');
            }
        }
        var P = local.Promise;
        if (P) {
            var promiseToString = null;
            try {
                promiseToString = Object.prototype.toString.call(P.resolve());
            }
            catch (e) {
            }
            if (promiseToString === '[object Promise]' && !P.cast) {
                return;
            }
        }
        local.Promise = Promise;
    }
    Promise.polyfill = polyfill;
    Promise.Promise = Promise;
    return Promise;
})));
ES6Promise.polyfill();
