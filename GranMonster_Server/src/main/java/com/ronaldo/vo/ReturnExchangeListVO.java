package com.ronaldo.vo;

import java.util.List;

import com.ronaldo.config.ErrorCodeConfig.ExchangeEnum;

public class ReturnExchangeListVO {
	private List<ReturnExchangeVO> exchangeList;
	private ExchangeEnum state;
	
	public List<ReturnExchangeVO> getExchangeList() {
		return exchangeList;
	}
	public void setExchangeList(List<ReturnExchangeVO> exchangeList) {
		this.exchangeList = exchangeList;
	}
	public ExchangeEnum getState() {
		return state;
	}
	public void setState(ExchangeEnum state) {
		this.state = state;
	}
}