package com.ronaldo.vo;

import com.ronaldo.config.GranConfig.AppTypeEnum;

public class ReceiveAppListVO {
    private String userKey;
    private String appKey;
    private AppTypeEnum appTypeEnum;
    
	public AppTypeEnum getAppTypeEnum() {
		return appTypeEnum;
	}
	public void setAppTypeEnum(AppTypeEnum appTypeEnum) {
		this.appTypeEnum = appTypeEnum;
	}
	public String getUserKey() {
		return userKey;
	}
	public void setUserKey(String userKey) {
		this.userKey = userKey;
	}
	public String getAppKey() {
		return appKey;
	}
	public void setAppKey(String appKey) {
		this.appKey = appKey;
	}
}