package com.ronaldo.vo;

import com.ronaldo.config.GranConfig.AppTypeEnum;

public class ReceiveExhaustVO {
	private String userKey;
	private String appKey;
	private int coin;
	private String payload;
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
	public int getCoin() {
		return coin;
	}
	public void setCoin(int coin) {
		this.coin = coin;
	}
	public String getPayload() {
		return payload;
	}
	public void setPayload(String payload) {
		this.payload = payload;
	}
	public AppTypeEnum getAppTypeEnum() {
		return appTypeEnum;
	}
	public void setAppTypeEnum(AppTypeEnum appTypeEnum) {
		this.appTypeEnum = appTypeEnum;
	}
}