package com.ronaldo.vo;

import com.ronaldo.config.GranConfig.AppTypeEnum;

public class ReceivePlayVO {
	private String userKey;
	private String appKey;
	private String desAppKey;
	private AppTypeEnum appTypeEnum;
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
	public String getDesAppKey() {
		return desAppKey;
	}
	public void setDesAppKey(String desAppKey) {
		this.desAppKey = desAppKey;
	}
	public AppTypeEnum getAppTypeEnum() {
		return appTypeEnum;
	}
	public void setAppTypeEnum(AppTypeEnum appTypeEnum) {
		this.appTypeEnum = appTypeEnum;
	}
	
}