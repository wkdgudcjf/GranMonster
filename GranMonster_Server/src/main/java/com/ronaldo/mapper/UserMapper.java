package com.ronaldo.mapper;

import java.util.List;

import org.apache.ibatis.annotations.Mapper;

import com.ronaldo.domain.UserDTO;

@Mapper
public interface UserMapper{
	 public List<UserDTO> getUserList();
	 public void registUser(UserDTO userDTO) throws Exception;
	 public UserDTO getUser(String userKey);
	 public void deleteUser(int userID) throws Exception;
	 public void updateUser(UserDTO userDTO) throws Exception;
	 public void updateUserPayload(UserDTO userDTO) throws Exception;
	 public UserDTO getUserByUserID(int userID);
}