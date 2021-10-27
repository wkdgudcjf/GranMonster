package com.ronaldo.mapper;


import java.util.List;

import org.apache.ibatis.annotations.Mapper;

import com.ronaldo.domain.AppRouteDTO;

@Mapper
public interface AppRouteMapper{
	public void registAppRoute(AppRouteDTO appRouteDTO) throws Exception;
	public List<AppRouteDTO> getAppRouteDTO(int appID);
}