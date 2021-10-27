package com.ronaldo.domain;

public class AppRouteDTO {
    private int appRouteID;
    private int srcAppID;
    private int desAppID;
    private boolean appRouteType;
	public int getAppRouteID() {
		return appRouteID;
	}
	public void setAppRouteID(int appRouteID) {
		this.appRouteID = appRouteID;
	}
	public int getSrcAppID() {
		return srcAppID;
	}
	public void setSrcAppID(int srcAppID) {
		this.srcAppID = srcAppID;
	}
	public int getDesAppID() {
		return desAppID;
	}
	public void setDesAppID(int desAppID) {
		this.desAppID = desAppID;
	}
	public boolean isAppRouteType() {
		return appRouteType;
	}
	public void setAppRouteType(boolean appRouteType) {
		this.appRouteType = appRouteType;
	}
}