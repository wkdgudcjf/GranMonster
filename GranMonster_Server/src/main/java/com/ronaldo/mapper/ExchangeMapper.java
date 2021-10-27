package com.ronaldo.mapper;

import java.util.List;

import org.apache.ibatis.annotations.Mapper;

import com.ronaldo.domain.ExchangeDTO;

@Mapper
public interface ExchangeMapper{
	 public ExchangeDTO getExchange(int exchangeID);
	 public List<ExchangeDTO> getExchangeList();
	 public List<ExchangeDTO> getEnableExchangeList(boolean exchangeEnable);
	 public void registExchange(ExchangeDTO exchangeDTO) throws Exception;
	 public void deleteExchange(int exchangeID) throws Exception;
	 public void updateExchange(ExchangeDTO exchangeDTO) throws Exception;
}