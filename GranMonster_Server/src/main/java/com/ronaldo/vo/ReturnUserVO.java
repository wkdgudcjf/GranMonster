package com.ronaldo.vo;

import com.ronaldo.config.ErrorCodeConfig.LoginEnum;

public class ReturnUserVO {
    private LoginEnum state;
    private String userEmail;
    private int userCoin;
    private boolean firstLogin;
	public LoginEnum getState() {
		return state;
	}
	public void setState(LoginEnum state) {
		this.state = state;
	}
	public String getUserEmail() {
		return userEmail;
	}
	public void setUserEmail(String userEmail) {
		this.userEmail = userEmail;
	}
	public int getUserCoin() {
		return userCoin;
	}
	public void setUserCoin(int userCoin) {
		this.userCoin = userCoin;
	}
	public boolean isFirstLogin() {
		return firstLogin;
	}
	public void setFirstLogin(boolean firstLogin) {
		this.firstLogin = firstLogin;
	}
}