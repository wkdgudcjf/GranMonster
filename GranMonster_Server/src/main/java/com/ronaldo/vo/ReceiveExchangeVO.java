package com.ronaldo.vo;

import org.springframework.web.multipart.MultipartFile;

public class ReceiveExchangeVO {
	private int exchangeMoney;
	private int exchangeCoin;
	private String exchangeKey;
	private String exchangeName;
	private MultipartFile exchangeHImage;
	private MultipartFile exchangeVImage;
	private int exchangeID;
	private boolean exchangeEnable;
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
	public String getExchangeKey() {
		return exchangeKey;
	}
	public void setExchangeKey(String exchangeKey) {
		this.exchangeKey = exchangeKey;
	}
	public String getExchangeName() {
		return exchangeName;
	}
	public void setExchangeName(String exchangeName) {
		this.exchangeName = exchangeName;
	}
	public MultipartFile getExchangeHImage() {
		return exchangeHImage;
	}
	public void setExchangeHImage(MultipartFile exchangeHImage) {
		this.exchangeHImage = exchangeHImage;
	}
	public MultipartFile getExchangeVImage() {
		return exchangeVImage;
	}
	public void setExchangeVImage(MultipartFile exchangeVImage) {
		this.exchangeVImage = exchangeVImage;
	}
	public int getExchangeID() {
		return exchangeID;
	}
	public void setExchangeID(int exchangeID) {
		this.exchangeID = exchangeID;
	}
	public boolean isExchangeEnable() {
		return exchangeEnable;
	}
	public void setExchangeEnable(boolean exchangeEnable) {
		this.exchangeEnable = exchangeEnable;
	}
	
}