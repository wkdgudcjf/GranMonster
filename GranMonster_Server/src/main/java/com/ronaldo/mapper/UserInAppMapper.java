package com.ronaldo.mapper;


import java.util.List;

import org.apache.ibatis.annotations.Mapper;

import com.ronaldo.domain.UserInAppDTO;

@Mapper
public interface UserInAppMapper{
	public List<UserInAppDTO> getUserInAppByUserID(int userID);
	public UserInAppDTO getUserInApp(UserInAppDTO userInAppDTO);
	public void registUserInApp(UserInAppDTO userInAppDTO) throws Exception;
	public List<UserInAppDTO> getUserInAppByAppID(int appID);
}