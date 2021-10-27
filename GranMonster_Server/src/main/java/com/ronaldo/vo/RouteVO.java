package com.ronaldo.vo;

public class RouteVO {
	private String appName;
	private int appID;
	private int install;
	private int play;
	
	public int getAppID() {
		return appID;
	}
	public void setAppID(int appID) {
		this.appID = appID;
	}
	public String getAppName() {
		return appName;
	}
	public void setAppName(String appName) {
		this.appName = appName;
	}
	public int getInstall() {
		return install;
	}
	public void setInstall(int install) {
		this.install = install;
	}
	public int getPlay() {
		return play;
	}
	public void setPlay(int play) {
		this.play = play;
	}
	public void incrementInstall() {
		this.install++;
	}
	public void incrementPlay() {
		this.play++;
	}
}