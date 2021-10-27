package com.ronaldo.domain;

public class ExchangeDTO {
    private int exchangeID;
    private int exchangeMoney;
    private int exchangeCoin;
    private boolean exchangeEnable;
    private String exchangeName;
    private String exchangeHImagePath;
    private String exchangeVImagePath;
    private String exchangeKey;
	public int getExchangeID() {
		return exchangeID;
	}
	public void setExchangeID(int exchangeID) {
		this.exchangeID = exchangeID;
	}
	public int getExchangeMoney() {
		return exchangeMoney;
	}
	public void setExchangeMoney(int exchangeMoney) {
		this.exchangeMoney = exchangeMoney;
	}
	public int getExchangeCoin() {
		return exchangeCoin;
	}
	public void setExchangeCoin(int exchangeCoin) {
		this.exchangeCoin = exchangeCoin;
	}
	public boolean isExchangeEnable() {
		return exchangeEnable;
	}
	public void setExchangeEnable(boolean exchangeEnable) {
		this.exchangeEnable = exchangeEnable;
	}
	public String getExchangeName() {
		return exchangeName;
	}
	public void setExchangeName(String exchangeName) {
		this.exchangeName = exchangeName;
	}
	public String getExchangeHImagePath() {
		return exchangeHImagePath;
	}
	public void setExchangeHImagePath(String exchangeHImagePath) {
		this.exchangeHImagePath = exchangeHImagePath;
	}
	public String getExchangeVImagePath() {
		return exchangeVImagePath;
	}
	public void setExchangeVImagePath(String exchangeVImagePath) {
		this.exchangeVImagePath = exchangeVImagePath;
	}
	public String getExchangeKey() {
		return exchangeKey;
	}
	public void setExchangeKey(String exchangeKey) {
		this.exchangeKey = exchangeKey;
	}
    
}