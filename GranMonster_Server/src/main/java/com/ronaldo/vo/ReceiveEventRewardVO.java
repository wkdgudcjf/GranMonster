package com.ronaldo.vo;

import com.ronaldo.config.GranConfig.AppTypeEnum;

public class ReceiveEventRewardVO {
	private String userKey;
	private String appKey;
	private String eventKey;
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
	public String getEventKey() {
		return eventKey;
	}
	public void setEventKey(String eventKey) {
		this.eventKey = eventKey;
	}
	public AppTypeEnum getAppTypeEnum() {
		return appTypeEnum;
	}
	public void setAppTypeEnum(AppTypeEnum appTypeEnum) {
		this.appTypeEnum = appTypeEnum;
	}
}