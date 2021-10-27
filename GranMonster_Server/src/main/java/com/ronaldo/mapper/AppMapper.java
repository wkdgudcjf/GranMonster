package com.ronaldo.mapper;

import java.util.List;

import org.apache.ibatis.annotations.Mapper;

import com.ronaldo.domain.AppDTO;

@Mapper
public interface AppMapper{
	public AppDTO getAppByID(int appID);
	public AppDTO getAppByKey(String appKey);
	 public List<AppDTO> getAppList();
	 public List<AppDTO> getEnableAppList(boolean appEnable);
	 public void registApp(AppDTO appDTO) throws Exception;
	 public void deleteApp(int appID) throws Exception;
	 public void updateApp(AppDTO appDTO) throws Exception;
	 public void modifyAppKey(AppDTO appDTO) throws Exception;
}