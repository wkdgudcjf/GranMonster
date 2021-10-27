package com.ronaldo.config;

public class GranConfig
{
	public static final String RETURN_IMAGE_FAIL = "Image is inconguity";
	
	public static final String RETURN_APP_REGIST_SECCESS = "App Regist Success!";
	public static final String RETURN_APP_REGIST_FAIL = "Already registed";
	public static final String RETURN_APP_MODIFY_SECCESS = "App Modify Success";
	public static final String RETURN_APP_MODIFY_FAIL = "Already registed";
	
	public static final String RETURN_APP_EVENT_REGIST_SECCESS = "Event Regist Success!";
	public static final String RETURN_APP_EVENT_REGIST_FAIL_EXCEL = "Please check the input Form";
	public static final String RETURN_APP_EVENT_REGIST_FAIL = "Already registed";
	public static final String RETURN_APP_EVENT_MODIFY_SECCESS = "Event Modify Success!";
	public static final String RETURN_APP_EVENT_MODIFY_FAIL = "Already registed";
	
	public static final String RETURN_EXCHANGE_REGIST_SECCESS = "Exchange Regist Success!";
	public static final String RETURN_EXCHANGE_REGIST_FAIL = "Already registed";
	public static final String RETURN_EXCHANGE_MODIFY_SECCESS ="Exchange Modify Success!";
	public static final String RETURN_EXCHANGE_MODIFY_FAIL = "Already registed";
	
	public static final String RETURN_COMPANY_REGIST_SECCESS = "Company Regist Success!";
	public static final String RETURN_COMPANY_REGIST_FAIL = "Already registed";
	public static final String RETURN_COMPANY_MODIFY_SECCESS = "Company Modify Success!";
	public static final String RETURN_COMPANY_MODIFY_FAIL = "Already registed";

	public static final int EVENT_CELL_NUMBER = 10;

	public static final String RETURN_APP_EVENT_SAME_KEY_EXCEL = "Exist same key";
	public static final String RETURN_APP_EVENT_LIMIT_EXCEL = "Current count > Limit count";
	public static final String RETURN_APP_EVENT_START_EXCEL = "Start time > End time";
	
	public enum AppTypeEnum
	{
		ANDROID,
		IPHONE;
	}
}
