package com.ronaldo.mapper;

import java.util.List;

import org.apache.ibatis.annotations.Mapper;

import com.ronaldo.domain.AppEventDTO;

@Mapper
public interface AppEventMapper{
	public List<AppEventDTO> getAppEventList();
	public List<AppEventDTO> getAppEventByAppID(int appID);
	public List<AppEventDTO> getAppEventByAppIDByEnable(int appID);
	public AppEventDTO getAppEventByEventID(int appEventID);
	public AppEventDTO getAppEventByAppIDByEventKey(AppEventDTO appEventDTO);
	public void registAppEvent(AppEventDTO appEventDTO) throws Exception;
	public void updateAppEvent(AppEventDTO appEventDTO) throws Exception;
	public void modifyEventCount(AppEventDTO appEventDTO);
	public void disableAppEvent(int appEventID);
}