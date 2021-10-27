package com.ronaldo.domain;

public class AuthUserDTO
{ 
	private String authUserID;
	private String authUserEmail;
	private String authUserPassword;
	private String authUserAuth;
	public AuthUserDTO(String authUserID, String authUserPassword, String authUserEmail, String authUserAuth) {
		this.authUserID = authUserID;
		this.authUserPassword = authUserPassword;
		this.authUserEmail = authUserEmail;
		this.authUserAuth = authUserAuth;
	}
	public String getAuthUserID() {
		return authUserID;
	}
	public void setAuthUserID(String authUserID) {
		this.authUserID = authUserID;
	}
	public String getAuthUserEmail() {
		return authUserEmail;
	}
	public void setAuthUserEmail(String authUserEmail) {
		this.authUserEmail = authUserEmail;
	}
	public String getAuthUserPassword() {
		return authUserPassword;
	}
	public void setAuthUserPassword(String authUserPassword) {
		this.authUserPassword = authUserPassword;
	}
	public String getAuthUserAuth() {
		return authUserAuth;
	}
	public void setAuthUserAuth(String authUserAuth) {
		this.authUserAuth = authUserAuth;
	}
}
