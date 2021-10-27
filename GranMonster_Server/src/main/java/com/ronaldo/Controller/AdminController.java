package com.ronaldo.Controller;

import java.io.File;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.sql.Timestamp;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.StringTokenizer;

import javax.servlet.ServletContext;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.web.servlet.View;

import com.ronaldo.Component.DownloadFileView;
import com.ronaldo.config.GranConfig;
import com.ronaldo.config.SessionWire;
import com.ronaldo.domain.AppEventDTO;
import com.ronaldo.domain.BillingDTO;
import com.ronaldo.domain.AppDTO;
import com.ronaldo.domain.CompanyDTO;
import com.ronaldo.domain.ExchangeDTO;
import com.ronaldo.domain.UserDTO;
import com.ronaldo.domain.UserEventDTO;
import com.ronaldo.domain.UserInAppDTO;
import com.ronaldo.service.ApiService;
import com.ronaldo.service.AuthUserService;
import com.ronaldo.vo.DashBoardVO;
import com.ronaldo.vo.ReceiveAppEventVO;
import com.ronaldo.vo.ReceiveAppVO;
import com.ronaldo.vo.ReceiveCompanyVO;
import com.ronaldo.vo.ReceiveExchangeVO;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@Controller
public class AdminController
{
	@Autowired
	private ServletContext context;
	@Autowired
	private AuthUserService userService;
	@Autowired
	private ApiService apiService;
	@Autowired
	private SessionWire sessionWire;
	
	private static final Logger LOG = LoggerFactory.getLogger(AdminController.class);
	 
	@RequestMapping(value = "/managementapp", method = RequestMethod.GET)
    public String managementapp(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementApp(model);
    }
	@RequestMapping(value = "/managementappinfo", method = RequestMethod.POST)
    public String managementappinfo(Model model,@RequestParam("appID") int appID)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementAppInfo(model,appID);
    }
	@RequestMapping(value = "/managementsdk", method = RequestMethod.GET)
    public String managementsdk(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementSDK(model,"managementsdk");
    }
	@RequestMapping(value = "/managementsdk_billing", method = RequestMethod.GET)
    public String managementsdk_billing(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementSDK(model,"managementsdk_billing");
    }
	@RequestMapping(value = "/managementsdk_billing_dialog", method = RequestMethod.GET)
    public String managementsdk_billing_dialog(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementSDK(model,"managementsdk_billing_dialog");
    }
	@RequestMapping(value = "/managementsdk_billing_button", method = RequestMethod.GET)
    public String managementsdk_billing_button(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementSDK(model,"managementsdk_billing_button");
    }
	@RequestMapping(value = "/managementsdk_billing_exhaust", method = RequestMethod.GET)
    public String managementsdk_billing_exhaust(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementSDK(model,"managementsdk_billing_exhaust");
    }
	@RequestMapping(value = "/managementsdk_widget_dialog", method = RequestMethod.GET)
    public String managementsdk_widget_dialog(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementSDK(model,"managementsdk_widget_dialog");
    }
	@RequestMapping(value = "/managementsdk_widget_button", method = RequestMethod.GET)
    public String managementsdk_widget_button(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementSDK(model,"managementsdk_widget_button");
    }
	@RequestMapping(value = "/managementsdk_login", method = RequestMethod.GET)
    public String managementsdk_login(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementSDK(model,"managementsdk_login");
    }
	
	@RequestMapping(value = "/managementsdk_event", method = RequestMethod.GET)
    public String managementsdk_event(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementSDK(model,"managementsdk_event");
    }
	
	@RequestMapping(value = "/managementsdk_errorcode", method = RequestMethod.GET)
    public String managementsdk_errorcode(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementSDK(model,"managementsdk_errorcode");
    }
	
	@RequestMapping(value = "/managementuser", method = RequestMethod.GET)
    public String managementuser(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementUser(model);
    }
	@RequestMapping(value = "/managementbilling", method = RequestMethod.GET)
    public String managementbilling(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	
    	return setManagementBilling(model);
    }
	@RequestMapping(value = "/managementexchange", method = RequestMethod.GET)
    public String managementgrancoin(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementExchange(model);
    }
	@RequestMapping(value = "/managementcompany", method = RequestMethod.GET)
    public String managementcompany(Model model)
	{
    	if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementCompany(model);
    }
	@RequestMapping(value = "/managementappevent", method = RequestMethod.POST)
    public String managementappevent(Model model,@RequestParam("appID") int appID) 
	{
		if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementAppEvent(model,appID);
    }
	@RequestMapping(value = "/managementuserinfo", method = RequestMethod.POST)
    public String managementuserinfo(Model model,@RequestParam("userID") int userID)
	{
		if(sessionWire.getId()==null)
		{
    		return setRedirectLogin(model);
		}
    	return setManagementUserInfo(model,userID);
    }
	class NestedImagePackage
	{
		byte[] imageBytes;
		Path imagePath;
		String fileName;
		public byte[] getImageBytes() {
			return imageBytes;
		}
		public void setImageBytes(byte[] imageBytes) {
			this.imageBytes = imageBytes;
		}
		public Path getImagePath() {
			return imagePath;
		}
		public void setImagePath(Path imagePath) {
			this.imagePath = imagePath;
		}
		public String getFileName() {
			return fileName;
		}
		public void setFileName(String fileName) {
			this.fileName = fileName;
		}
	}
	@RequestMapping(value = "/downloadSDK", method = RequestMethod.POST)
    public View downLoadFile() {
        String fullPath = context.getRealPath("sdk/Granmonster_SDK_v1.7.unitypackage");
        File downloadFile = new File(fullPath);
        DownloadFileView downloadFileView = new DownloadFileView();
        downloadFileView.downloadFileName = "Granmonster_SDK_v1.7.unitypackage";
        downloadFileView.file = downloadFile;
        return downloadFileView;
    }

	@RequestMapping(value = "/registapp", method = RequestMethod.POST)
    public ResponseEntity<String> registApp(@ModelAttribute ReceiveAppVO receiveAppVO)
	{
        try {
        	// Need S3 File Server ... 
        	 // Get the file and save it uploads dir
        	NestedImagePackage nestedImageIconPackage = getImagePath(receiveAppVO.getAppIconImage(),"image/appIcon/","_v1_icon",receiveAppVO.getAppName());
        	NestedImagePackage nestedImageHBPackage1 = getImagePath(receiveAppVO.getAppHBannerImage1(),"image/appHBanner1/","_v1_Hbanner",receiveAppVO.getAppName());
        	NestedImagePackage nestedImageVBPackage1 = getImagePath(receiveAppVO.getAppVBannerImage1(),"image/appVBanner1/","_v1_Vbanner",receiveAppVO.getAppName());
        	NestedImagePackage nestedImageHBPackage2 = getImagePath(receiveAppVO.getAppHBannerImage2(),"image/appHBanner2/","_v1_Hbanner",receiveAppVO.getAppName());
        	NestedImagePackage nestedImageVBPackage2 = getImagePath(receiveAppVO.getAppVBannerImage2(),"image/appVBanner2/","_v1_Vbanner",receiveAppVO.getAppName());
        	NestedImagePackage nestedImageHBPackage3 = getImagePath(receiveAppVO.getAppHBannerImage3(),"image/appHBanner3/","_v1_Hbanner",receiveAppVO.getAppName());
        	NestedImagePackage nestedImageVBPackage3 = getImagePath(receiveAppVO.getAppVBannerImage3(),"image/appVBanner3/","_v1_Vbanner",receiveAppVO.getAppName());
        	
             if(apiService.registApp(receiveAppVO,nestedImageIconPackage.getFileName(),nestedImageHBPackage1.getFileName()
            		 ,nestedImageVBPackage1.getFileName(),nestedImageHBPackage2.getFileName()
            		 ,nestedImageVBPackage2.getFileName(),nestedImageHBPackage3.getFileName()
            		 ,nestedImageVBPackage3.getFileName()))
             {
                 Files.write(nestedImageIconPackage.getImagePath(), nestedImageIconPackage.getImageBytes());
                 Files.write(nestedImageHBPackage1.getImagePath(), nestedImageHBPackage1.getImageBytes());
                 Files.write(nestedImageVBPackage1.getImagePath(), nestedImageVBPackage1.getImageBytes());
                 Files.write(nestedImageHBPackage2.getImagePath(), nestedImageHBPackage2.getImageBytes());
                 Files.write(nestedImageVBPackage2.getImagePath(), nestedImageVBPackage2.getImageBytes());
                 Files.write(nestedImageHBPackage3.getImagePath(), nestedImageHBPackage3.getImageBytes());
                 Files.write(nestedImageVBPackage3.getImagePath(), nestedImageVBPackage3.getImageBytes());
            	 return new ResponseEntity<>(GranConfig.RETURN_APP_REGIST_SECCESS,HttpStatus.OK);
             }
             else
             {
            	 return new ResponseEntity<>(GranConfig.RETURN_APP_REGIST_FAIL,HttpStatus.BAD_REQUEST);
             }
        } catch (Exception e) {
        	LOG.info(e.getMessage());
            return new ResponseEntity<>(GranConfig.RETURN_IMAGE_FAIL,HttpStatus.BAD_REQUEST);
        }
    }
	private NestedImagePackage getImagePath(MultipartFile multipartFile,String path,String fileName,String appName) throws Exception
	{
		NestedImagePackage nestedImagePackage = new NestedImagePackage();
		byte[] imageBytes = multipartFile.getBytes();
		String imageOriginalFileName = multipartFile.getOriginalFilename();
        String imageOriginalFileExtension = imageOriginalFileName.substring(imageOriginalFileName.lastIndexOf("."));
        String imageFileName = appName+fileName+imageOriginalFileExtension;
        Path imagePath = Paths.get(context.getRealPath(path) + imageFileName);
        
        nestedImagePackage.setFileName(imageFileName);
        nestedImagePackage.setImageBytes(imageBytes);
        nestedImagePackage.setImagePath(imagePath);
        
		return nestedImagePackage;
	}
	@RequestMapping(value = "/modifyapp", method = RequestMethod.POST)
    public ResponseEntity<String> modifyApp(@ModelAttribute ReceiveAppVO receiveAppVO)
	{
        try {
        	// Need S3 File Server ... 
        	 // Get the file and save it uploads dir
			 AppDTO appDTO = apiService.getApp(receiveAppVO.getAppID());
			 String imageIconPath = appDTO.getAppImageIconPath();
			 String imageHBannerPath1 = appDTO.getAppImageHBannerPath1();
			 String imageVBannerPath1 = appDTO.getAppImageVBannerPath1();
			 String imageHBannerPath2 = appDTO.getAppImageHBannerPath2();
			 String imageVBannerPath2 = appDTO.getAppImageVBannerPath2();
			 String imageHBannerPath3 = appDTO.getAppImageHBannerPath3();
			 String imageVBannerPath3 = appDTO.getAppImageVBannerPath3();
			 
			 byte[] iconBytes = receiveAppVO.getAppIconImage().getBytes();
			 byte[] HbannerBytes1 = receiveAppVO.getAppHBannerImage1().getBytes();
			 byte[] VbannerBytes1 = receiveAppVO.getAppVBannerImage1().getBytes();
			 byte[] HbannerBytes2 = receiveAppVO.getAppHBannerImage2().getBytes();
			 byte[] VbannerBytes2 = receiveAppVO.getAppVBannerImage2().getBytes();
			 byte[] HbannerBytes3 = receiveAppVO.getAppHBannerImage3().getBytes();
			 byte[] VbannerBytes3 = receiveAppVO.getAppVBannerImage3().getBytes();
			 
			 NestedImagePackage nestedImageIconPackage = null;
             NestedImagePackage nestedImageHBPackage1 = null;
	         NestedImagePackage nestedImageVBPackage1 = null;
	         NestedImagePackage nestedImageHBPackage2 = null;
	         NestedImagePackage nestedImageVBPackage2 = null;
	         NestedImagePackage nestedImageHBPackage3 = null;
	         NestedImagePackage nestedImageVBPackage3 = null;
	        	
			 if(iconBytes.length != 0)
			 {
				 StringTokenizer stk = new StringTokenizer(imageIconPath,"_v");
				 String str1 = stk.nextToken();
				 String str2 = stk.nextToken();
				 int ver = str2.charAt(0)-48;
				 ver++;
				 nestedImageIconPackage = getImagePath(receiveAppVO.getAppIconImage(),"image/appIcon/","_v"+ver+"_icon",receiveAppVO.getAppName());
				 imageIconPath = nestedImageIconPackage.getFileName();
			 }
			 if(HbannerBytes1.length != 0)
			 {
				 StringTokenizer stk = new StringTokenizer(imageHBannerPath1,"_v");
				 String str1 = stk.nextToken();
				 String str2 = stk.nextToken();
				 int ver = str2.charAt(0)-48;
				 ver++;
				 nestedImageHBPackage1 = getImagePath(receiveAppVO.getAppHBannerImage1(),"image/appHBanner1/","_v"+ver+"_Hbanner",receiveAppVO.getAppName());
				 imageHBannerPath1 = nestedImageHBPackage1.getFileName();
			 }
			 if(VbannerBytes1.length != 0)
			 {
				 StringTokenizer stk = new StringTokenizer(imageVBannerPath1,"_v");
				 String str1 = stk.nextToken();
				 String str2 = stk.nextToken();
				 int ver = str2.charAt(0)-48;
				 ver++;
				 nestedImageVBPackage1 = getImagePath(receiveAppVO.getAppVBannerImage1(),"image/appVBanner1/","_v"+ver+"_Vbanner",receiveAppVO.getAppName());
				 imageVBannerPath1 = nestedImageVBPackage1.getFileName();
			 }
			 if(HbannerBytes2.length != 0)
			 {
				 StringTokenizer stk = new StringTokenizer(imageHBannerPath2,"_v");
				 String str1 = stk.nextToken();
				 String str2 = stk.nextToken();
				 int ver = str2.charAt(0)-48;
				 ver++;
				 nestedImageHBPackage2 = getImagePath(receiveAppVO.getAppHBannerImage2(),"image/appHBanner2/","_v"+ver+"_Hbanner",receiveAppVO.getAppName());
				 imageHBannerPath2 = nestedImageHBPackage2.getFileName();
			 }
			 if(VbannerBytes2.length != 0)
			 {
				 StringTokenizer stk = new StringTokenizer(imageVBannerPath2,"_v");
				 String str1 = stk.nextToken();
				 String str2 = stk.nextToken();
				 int ver = str2.charAt(0)-48;
				 ver++;
				 nestedImageVBPackage2 = getImagePath(receiveAppVO.getAppVBannerImage2(),"image/appVBanner2/","_v"+ver+"_Vbanner",receiveAppVO.getAppName());
				 imageVBannerPath2 = nestedImageVBPackage2.getFileName();
			 }
			 if(HbannerBytes3.length != 0)
			 {
				 StringTokenizer stk = new StringTokenizer(imageHBannerPath3,"_v");
				 String str1 = stk.nextToken();
				 String str2 = stk.nextToken();
				 int ver = str2.charAt(0)-48;
				 ver++;
				 nestedImageHBPackage3 = getImagePath(receiveAppVO.getAppHBannerImage3(),"image/appHBanner3/","_v"+ver+"_Hbanner",receiveAppVO.getAppName());
				 imageHBannerPath3 = nestedImageHBPackage3.getFileName();
			 }
			 if(VbannerBytes3.length != 0)
			 {
				 StringTokenizer stk = new StringTokenizer(imageVBannerPath3,"_v");
				 String str1 = stk.nextToken();
				 String str2 = stk.nextToken();
				 int ver = str2.charAt(0)-48;
				 ver++;
				 nestedImageVBPackage3 = getImagePath(receiveAppVO.getAppVBannerImage3(),"image/appVBanner3/","_v"+ver+"_Vbanner",receiveAppVO.getAppName());
				 imageVBannerPath3 = nestedImageVBPackage3.getFileName();
			 }
			if(apiService.modifyApp(receiveAppVO, imageIconPath,imageHBannerPath1,imageVBannerPath1
					,imageHBannerPath2,imageVBannerPath2,imageHBannerPath3,imageVBannerPath3))
			{
				if(iconBytes.length != 0)
				{
					Files.write(nestedImageIconPackage.getImagePath(), nestedImageIconPackage.getImageBytes());
				}
				if(HbannerBytes1.length != 0)
				{
					 Files.write(nestedImageHBPackage1.getImagePath(), nestedImageHBPackage1.getImageBytes());
				}
				if(VbannerBytes1.length != 0)
				{
					Files.write(nestedImageVBPackage1.getImagePath(), nestedImageVBPackage1.getImageBytes());
				}
				if(HbannerBytes2.length != 0)
				{
					 Files.write(nestedImageHBPackage2.getImagePath(), nestedImageHBPackage2.getImageBytes());
				}
				if(VbannerBytes2.length != 0)
				{
					Files.write(nestedImageVBPackage2.getImagePath(), nestedImageVBPackage2.getImageBytes());
				}
				if(HbannerBytes3.length != 0)
				{
					 Files.write(nestedImageHBPackage3.getImagePath(), nestedImageHBPackage3.getImageBytes());
				}
				if(VbannerBytes3.length != 0)
				{
					Files.write(nestedImageVBPackage3.getImagePath(), nestedImageVBPackage3.getImageBytes());
				}
				return new ResponseEntity<>(GranConfig.RETURN_APP_MODIFY_SECCESS,HttpStatus.OK);
			}
			else
			{
				return new ResponseEntity<>(GranConfig.RETURN_APP_MODIFY_FAIL,HttpStatus.BAD_REQUEST);
			}
        } catch (Exception e) {
        	LOG.info(e.getMessage());
            return new ResponseEntity<>(GranConfig.RETURN_IMAGE_FAIL,HttpStatus.BAD_REQUEST);
        }
    }
	@RequestMapping(value = "/getapp", method = RequestMethod.POST)
    public ResponseEntity<AppDTO> getApp(@RequestParam("appID") int appID)
	{
        return new ResponseEntity<>(apiService.getApp(appID),HttpStatus.OK);
    }
	@RequestMapping(value = "/getappevent", method = RequestMethod.POST)
    public ResponseEntity<AppEventDTO> getAppEvent(@RequestParam("appEventID") int appEventID)
	{
		AppEventDTO appEventDTO = apiService.getAppEvent(appEventID);
        return new ResponseEntity<>(appEventDTO,HttpStatus.OK);
    }
	@RequestMapping(value = "/dashboard", method = RequestMethod.POST)
    public ResponseEntity<List<DashBoardVO>> dashboard(@RequestParam("searchTime") String searchTime)
	{
		Timestamp startTimeStamp;
		Timestamp endTimeStamp;
		if(searchTime==null || searchTime.equals(""))
		{
			Date date = new Date();
			Calendar cal = Calendar.getInstance(); // locale-specific
			cal.setTime(date);
			cal.set(Calendar.HOUR_OF_DAY, 0);
			cal.set(Calendar.MINUTE, 0);
			cal.set(Calendar.SECOND, 0);
			cal.set(Calendar.MILLISECOND, 0);
			startTimeStamp = new Timestamp(cal.getTimeInMillis());
			cal.set(Calendar.HOUR_OF_DAY, 23);
			cal.set(Calendar.MINUTE, 59);
			cal.set(Calendar.SECOND, 59);
			endTimeStamp = new Timestamp(cal.getTimeInMillis());
		}
		else
		{
			startTimeStamp = Timestamp.valueOf(searchTime.substring(0,16)+":00");
			endTimeStamp = Timestamp.valueOf(searchTime.substring(19,35)+":00");
		}
		ArrayList<DashBoardVO> dashBoardVOlist = new ArrayList<DashBoardVO>();
		apiService.dashBoard(dashBoardVOlist,startTimeStamp,endTimeStamp);
        return new ResponseEntity<>(dashBoardVOlist,HttpStatus.OK);
    }
	@RequestMapping(value = "/registappevent", method = RequestMethod.POST)
    public ResponseEntity<String> registappevent(@ModelAttribute ReceiveAppEventVO receiveAppEventVO)
	{
        if(apiService.registAppEvent(receiveAppEventVO))
        {
           return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_REGIST_SECCESS,HttpStatus.OK);
        }
        return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_REGIST_FAIL,HttpStatus.BAD_REQUEST);
    }
	@RequestMapping(value = "/modifyappevent", method = RequestMethod.POST)
    public ResponseEntity<String> modifyappevent(@ModelAttribute ReceiveAppEventVO receiveAppEventVO) 
	{
		if(apiService.modifyAppEvent(receiveAppEventVO))
		{
			 return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_MODIFY_SECCESS,HttpStatus.OK);
		}
        return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_MODIFY_FAIL,HttpStatus.BAD_REQUEST);
    }
	@RequestMapping(value = "/registexchange", method = RequestMethod.POST)
    public ResponseEntity<String> registExchange(@ModelAttribute ReceiveExchangeVO receiveExchangeVO) 
	{
		// Need S3 File Server ... 
		 try {
             byte[] HBytes = receiveExchangeVO.getExchangeHImage().getBytes();
        	 String HOriginalFileName = receiveExchangeVO.getExchangeHImage().getOriginalFilename();
             String HOriginalFileExtension = HOriginalFileName.substring(HOriginalFileName.lastIndexOf("."));
             String HFileName = receiveExchangeVO.getExchangeName()+"_v1_H"+HOriginalFileExtension;
             Path HExchangePath = Paths.get(context.getRealPath("image/HExchange/") + HFileName);
             
             byte[] VBytes = receiveExchangeVO.getExchangeVImage().getBytes();
        	 String VOriginalFileName = receiveExchangeVO.getExchangeVImage().getOriginalFilename();
             String VOriginalFileExtension = VOriginalFileName.substring(VOriginalFileName.lastIndexOf("."));
             String VFileName = receiveExchangeVO.getExchangeName()+"_v1_V"+VOriginalFileExtension;
             Path VExchangePath = Paths.get(context.getRealPath("image/VExchange/") + VFileName);
             
             if(apiService.registExchange(receiveExchangeVO,HFileName,VFileName))
             {
                 Files.write(HExchangePath, HBytes);
                 Files.write(VExchangePath, VBytes);
            	 return new ResponseEntity<>(GranConfig.RETURN_EXCHANGE_REGIST_SECCESS,HttpStatus.OK);
             }
             else
             {
            	 return new ResponseEntity<>(GranConfig.RETURN_EXCHANGE_REGIST_FAIL,HttpStatus.BAD_REQUEST);
             }
        } catch (Exception e) {
        	LOG.info(e.getMessage());
            return new ResponseEntity<>(GranConfig.RETURN_IMAGE_FAIL,HttpStatus.BAD_REQUEST);
        }
    }
	@RequestMapping(value = "/modifyexchange", method = RequestMethod.POST)
    public ResponseEntity<String> modifyExchange(@ModelAttribute ReceiveExchangeVO receiveExchangeVO)
	{
		// Need S3 File Server ... 
        try {
       	 // Get the file and save it uploads dir
        	ExchangeDTO exchangeDTO = apiService.getExchange(receiveExchangeVO.getExchangeID());
			 String HImagePath = exchangeDTO.getExchangeHImagePath();
			 String VImagePath = exchangeDTO.getExchangeVImagePath();
			 byte[] Hbytes = receiveExchangeVO.getExchangeHImage().getBytes();
			 byte[] Vbytes = receiveExchangeVO.getExchangeVImage().getBytes();
			 if(Hbytes.length != 0)
			 {
				 String originalFileName = receiveExchangeVO.getExchangeHImage().getOriginalFilename();
	             String originalFileExtension = originalFileName.substring(originalFileName.lastIndexOf("."));
				 StringTokenizer stk = new StringTokenizer(HImagePath,"_v");
				 String str1 = stk.nextToken();
				 String str2 = stk.nextToken();
				 int ver = str2.charAt(0)-48;
				 ver++;
				 HImagePath = receiveExchangeVO.getExchangeName()+"_v"+ver+"_H"+originalFileExtension;
			 }
			 if(Vbytes.length != 0)
			 {
				 String originalFileName = receiveExchangeVO.getExchangeVImage().getOriginalFilename();
	             String originalFileExtension = originalFileName.substring(originalFileName.lastIndexOf("."));
				 StringTokenizer stk = new StringTokenizer(VImagePath,"_v");
				 String str1 = stk.nextToken();
				 String str2 = stk.nextToken();
				 int ver = str2.charAt(0)-48;
				 ver++;
				 VImagePath = receiveExchangeVO.getExchangeName()+"_v"+ver+"_V"+originalFileExtension;
			 }
			if(apiService.modifyExchange(receiveExchangeVO,HImagePath,VImagePath))
			{
				if(Hbytes.length != 0)
				{
					Path Hpath = Paths.get(context.getRealPath("image/HExchange/") + HImagePath);
				    Files.write(Hpath, Hbytes);
				}
				if(Vbytes.length != 0)
				{
					Path Vpath = Paths.get(context.getRealPath("image/VExchange/") + VImagePath);
				    Files.write(Vpath, Vbytes);
				}
				return new ResponseEntity<>(GranConfig.RETURN_EXCHANGE_MODIFY_SECCESS,HttpStatus.OK);
			}
			else
			{
				return new ResponseEntity<>(GranConfig.RETURN_EXCHANGE_MODIFY_FAIL,HttpStatus.BAD_REQUEST);
			}
       } catch (Exception e) {
    	   LOG.info(e.getMessage());
           return new ResponseEntity<>(GranConfig.RETURN_IMAGE_FAIL,HttpStatus.BAD_REQUEST);
       }
    }
	@RequestMapping(value = "/getexchange", method = RequestMethod.POST)
    public ResponseEntity<ExchangeDTO> getExchange(@RequestParam("exchangeID") int exchangeID)
	{
        return new ResponseEntity<>(apiService.getExchange(exchangeID),HttpStatus.OK);
    }
	@RequestMapping(value = "/registcompany", method = RequestMethod.POST)
    public ResponseEntity<String> registCompany(@ModelAttribute ReceiveCompanyVO receiveCompanyVO)
	{
		if(apiService.registCompany(receiveCompanyVO))
		{
			 return new ResponseEntity<>(GranConfig.RETURN_COMPANY_REGIST_SECCESS,HttpStatus.OK);
		}
        return new ResponseEntity<>(GranConfig.RETURN_COMPANY_REGIST_FAIL,HttpStatus.BAD_REQUEST);
    }
	@RequestMapping(value = "/modifycompany", method = RequestMethod.POST)
    public ResponseEntity<String> modifyCompany(@ModelAttribute ReceiveCompanyVO receiveCompanyVO)
	{
		if(apiService.modifyCompany(receiveCompanyVO))
		{
			 return new ResponseEntity<>(GranConfig.RETURN_COMPANY_MODIFY_SECCESS,HttpStatus.OK);
		}
        return new ResponseEntity<>(GranConfig.RETURN_COMPANY_MODIFY_FAIL,HttpStatus.BAD_REQUEST);
    }
	@RequestMapping(value = "/getcompany", method = RequestMethod.POST)
    public ResponseEntity<CompanyDTO> getCompany(@RequestParam("companyID") int companyID)
	{
        return new ResponseEntity<>(apiService.getCompany(companyID),HttpStatus.OK);
    }
	
	private String setManagementApp(Model model)
    {
    	model.addAttribute("user",userService.searchAuthUser(sessionWire.getId()));
    	model.addAttribute("companylist",apiService.getCompanyList());
    	model.addAttribute("applist",apiService.getAppList());
    	return "managementapp";
    }
	private String setManagementAppInfo(Model model,int appID)
    {
    	model.addAttribute("user",userService.searchAuthUser(sessionWire.getId()));
    	model.addAttribute("companylist",apiService.getCompanyList());
    	model.addAttribute("app",apiService.getApp(appID));
    	model.addAttribute("routeList",apiService.getAppRoute(appID));
    	return "managementappinfo";
    }
	private String setManagementCompany(Model model)
    {
    	model.addAttribute("user",userService.searchAuthUser(sessionWire.getId()));
    	model.addAttribute("companylist",apiService.getCompanyList());
    	return "managementcompany";
    }
	private String setManagementAppEvent(Model model,int appID)
    {
		model.addAttribute("app",apiService.getApp(appID));
		List<AppEventDTO> appEventList = apiService.getAppEventList(appID);
		for(int i=0;i<appEventList.size();i++)
		{
			appEventList.get(i).setAppEventOneoffs(appEventList.get(i).isAppEventOneoff()?"O":"X");
			if(appEventList.get(i).getAppEventEndTime().getTime() < System.currentTimeMillis() && appEventList.get(i).isAppEventEnable())// 시간지났고 enable이면.
			{
				apiService.disableAppEvent(appEventList.get(i).getAppEventID());
				appEventList.get(i).setAppEventEnable(false);
				LOG.info("setManagementAppEvent(ALREADY_EVENT_END) - AppID : " + appID+" /eventKey : "+appEventList.get(i).getAppEventKey());
				continue;
			}
		}
    	model.addAttribute("eventList",appEventList);
    	return "managementappevent";
    }
	private String setManagementUser(Model model)
    {
    	model.addAttribute("user",userService.searchAuthUser(sessionWire.getId()));
    	List<UserDTO> userDTOList = apiService.getUserList();
    	for(int i=0;i<userDTOList.size();i++)
    	{
    		if(userDTOList.get(i).isUserType() == true)
    		{
    			userDTOList.get(i).setUserTypeString("Android");
    		}
    		else
    		{
    			userDTOList.get(i).setUserTypeString("IOS");
    		}
    	}
    	model.addAttribute("userlist",userDTOList);
    	return "managementuser";
    }
	private String setManagementUserInfo(Model model,int userID)
    {
		// DTO -> VO 변경 필요... table과 너무 안맞게 되어있다... 브라우저 VO 따로 설계 / DTO db insert용으로만 바꾸기 모든 Select VO로 Return.
		List<UserInAppDTO> userInAppList = apiService.getUserInAppByUserID(userID);
		List<UserEventDTO> userEventList = apiService.getUserEventList(userID);
		for(int i=0;i<userInAppList.size();i++)
		{
			List<AppEventDTO> appEventList = apiService.getAppEventList(userInAppList.get(i).getAppID());
			for(int j=0;j<appEventList.size();j++)
			{
				appEventList.get(j).setAppEventRewardEnable("X");
				appEventList.get(j).setAppEventSuccessEnable("X");
				for(int k=0;k<userEventList.size();k++)
				{
					if(userEventList.get(k).getAppEventID()==appEventList.get(j).getAppEventID())
					{
						appEventList.get(j).setAppEventSuccessEnable("O");
						if(userEventList.get(k).isUserEventEnable())
						{
							appEventList.get(j).setAppEventRewardEnable("O");
						}
						break;
					}
				}
			}
			userInAppList.get(i).setAppEventList(appEventList);
		}
		model.addAttribute("user",apiService.getUser(userID));
		model.addAttribute("userinapplist",userInAppList);
    	return "managementuserinfo";
    }
	private String setManagementBilling(Model model)
    {
    	model.addAttribute("user",userService.searchAuthUser(sessionWire.getId()));
    	List<BillingDTO> billingList = apiService.getBillingList();
    	for(int i=0;i<billingList.size();i++)
    	{
    		if(billingList.get(i).isAppType() == true)
    		{
    			billingList.get(i).setAppTypeString("Android");
    		}
    		else
    		{
    			billingList.get(i).setAppTypeString("IOS");
    		}
    	}
    	model.addAttribute("billinglist",billingList);
    	return "managementbilling";
    }
	private String setManagementExchange(Model model)
    {
    	model.addAttribute("user",userService.searchAuthUser(sessionWire.getId()));
    	model.addAttribute("exchangelist",apiService.getExchangeList());
    	return "managementexchange";
    }
	private String setRedirectLogin(Model model)
    {
    	model.addAttribute("message", "Gran Monster");
    	return "redirect:/login";
    }
	private String setManagementSDK(Model model,String Site) {
		model.addAttribute("user",userService.searchAuthUser(sessionWire.getId()));
    	return Site;
	}
}
