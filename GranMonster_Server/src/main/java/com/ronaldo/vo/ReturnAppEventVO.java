package com.ronaldo.vo;

public class ReturnAppEventVO {
    private String appEventContent;
	private int appEventCoin;
	private String appEventKey;
	private boolean appEventSuccessEnable;
	private boolean appEventRewardEnable;
	public String getAppEventKey() {
		return appEventKey;
	}
	public void setAppEventKey(String appEventKey) {
		this.appEventKey = appEventKey;
	}
	public String getAppEventContent() {
		return appEventContent;
	}
	public void setAppEventContent(String appEventContent) {
		this.appEventContent = appEventContent;
	}
	public int getAppEventCoin() {
		return appEventCoin;
	}
	public void setAppEventCoin(int appEventCoin) {
		this.appEventCoin = appEventCoin;
	}
	public boolean isAppEventSuccessEnable() {
		return appEventSuccessEnable;
	}
	public void setAppEventSuccessEnable(boolean appEventSuccessEnable) {
		this.appEventSuccessEnable = appEventSuccessEnable;
	}
	public boolean isAppEventRewardEnable() {
		return appEventRewardEnable;
	}
	public void setAppEventRewardEnable(boolean appEventRewardEnable) {
		this.appEventRewardEnable = appEventRewardEnable;
	}
	
	
}