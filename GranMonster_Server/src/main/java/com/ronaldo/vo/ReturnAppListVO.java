package com.ronaldo.vo;

import java.util.List;

import com.ronaldo.config.ErrorCodeConfig.AppListEnum;

public class ReturnAppListVO {
    private AppListEnum state;
	private List<ReturnAppVO> appList;
	private int userCoin;
	public AppListEnum getState() {
		return state;
	}
	public void setState(AppListEnum state) {
		this.state = state;
	}
	public List<ReturnAppVO> getAppList() {
		return appList;
	}
	public void setAppList(List<ReturnAppVO> appList) {
		this.appList = appList;
	}
	public int getUserCoin() {
		return userCoin;
	}
	public void setUserCoin(int userCoin) {
		this.userCoin = userCoin;
	}
}