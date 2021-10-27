package com.ronaldo.mapper;

import java.util.List;

import org.apache.ibatis.annotations.Mapper;

import com.ronaldo.domain.UserEventDTO;

@Mapper
public interface UserEventMapper{
	public UserEventDTO getUserEvent(UserEventDTO userEventDTO);
	public void registUserEvent(UserEventDTO userEventDTO);
	public List<UserEventDTO> getUserEventList(int userID);
	public void updateUserEvent(UserEventDTO userEventDTO);
	public void deleteUserEvent(int userID);
}