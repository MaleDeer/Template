/// <reference path="./wxAPI.d.ts" />

import { Config } from "./Config";
import { Wx_GetLoginCode } from "./Common/Service/NetServiceBase";

export class _Application {

  public globalData = {
    userInfo: null
  };

  public async onLaunch() {

  }
}

App(new _Application());