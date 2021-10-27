package com.ronaldo.Controller;

import com.ronaldo.config.GranConfig;
import com.ronaldo.domain.AppEventDTO;
import com.ronaldo.service.ApiService;
import com.ronaldo.util.ExcelRead;
import com.ronaldo.vo.ReceviceAppEventExcelVO;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.jdbc.datasource.DataSourceTransactionManager;
import org.springframework.stereotype.Controller;
import org.springframework.transaction.TransactionDefinition;
import org.springframework.transaction.TransactionStatus;
import org.springframework.transaction.support.DefaultTransactionDefinition;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.multipart.MultipartFile;

import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@Controller
public class UploadExcelController {
	
	@Autowired
	private ApiService apiService;
	
	@Autowired
	private DataSourceTransactionManager dataSourceTransactionManager;
	
	private static final Logger LOG = LoggerFactory.getLogger(UploadExcelController.class);
	
	@RequestMapping(value = "/excelUpload", method = RequestMethod.POST)
    public ResponseEntity<String> readExcel(@RequestParam("enrollAppEventExcel") MultipartFile multipartFile,
    									@RequestParam("enrollAppID") int appID)
	{
		SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm");
		DefaultTransactionDefinition defaultTransactionDefinition = new DefaultTransactionDefinition();
		defaultTransactionDefinition.setName("readExcel");
		defaultTransactionDefinition.setPropagationBehavior(TransactionDefinition.PROPAGATION_REQUIRED);
		TransactionStatus transactionStatus=null;
		try 
		{
			transactionStatus = dataSourceTransactionManager.getTransaction(defaultTransactionDefinition);
			List<ReceviceAppEventExcelVO> excelList = ExcelRead.readExcelToList(multipartFile, ReceviceAppEventExcelVO::rowOf);
			for(int i=0;i<excelList.size()-1;i++)
			{
				for(int j=i+1;j<excelList.size();j++)
				{
					if(excelList.get(i).getAppEventKey().compareTo(excelList.get(j).getAppEventKey())==0)
					{
						LOG.info("ExcelUpload - Exist same Event Key : "+excelList.get(i).getAppEventKey());
						return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_SAME_KEY_EXCEL,HttpStatus.BAD_REQUEST);
					}
				}
			}
			
			List<AppEventDTO> eventList = apiService.getAppEventList(appID);
			
			for(int i=0;i<excelList.size();i++)
			{
				if(excelList.get(i).getAppEventID()==0) // 새로 등록
				{
					if(excelList.get(i).getAppEventLimit() < excelList.get(i).getAppEventCount() && excelList.get(i).getAppEventLimit()!=0)
					{
						dataSourceTransactionManager.rollback(transactionStatus);
						LOG.info("getAppEventLimit - limit count");
						return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_LIMIT_EXCEL,HttpStatus.BAD_REQUEST);
					}
					if(excelList.get(i).getAppEventStartTime().getTime() > excelList.get(i).getAppEventEndTime().getTime())
					{
						dataSourceTransactionManager.rollback(transactionStatus);
						LOG.info("getAppEventLimit - start time > end time");
						return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_START_EXCEL,HttpStatus.BAD_REQUEST);
					}
					AppEventDTO appEventDTO = new AppEventDTO();
					appEventDTO.setAppID(appID);
					String enableStr = excelList.get(i).getAppEventEnable();
					boolean enable = false;
					if(enableStr.compareTo("O")==0)
					{
						enable = true;
					}
					String oneOffStr = excelList.get(i).getAppEventOffsets();
					boolean enableOneoff = false;
					if(oneOffStr.compareTo("O")==0)
					{
						enableOneoff = true;
					}
					appEventDTO.setAppEventEnable(enable);
					appEventDTO.setAppEventKey(excelList.get(i).getAppEventKey());
					appEventDTO.setAppEventContent(excelList.get(i).getAppEventContent());
					appEventDTO.setAppEventCoin(excelList.get(i).getAppEventCoin());
					appEventDTO.setAppEventStartTime(Timestamp.valueOf(sdf.format(excelList.get(i).getAppEventStartTime())+":00"));
					appEventDTO.setAppEventEndTime(Timestamp.valueOf(sdf.format(excelList.get(i).getAppEventEndTime())+":00"));
					appEventDTO.setAppEventLimit(excelList.get(i).getAppEventLimit());
					appEventDTO.setAppEventOneoff(enableOneoff);
					if(apiService.registAppEventByExcel(appEventDTO)==false)
					{
						dataSourceTransactionManager.rollback(transactionStatus);
						LOG.info("registAppEventByExcel - error");
						return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_REGIST_FAIL_EXCEL,HttpStatus.BAD_REQUEST);
					}
				}
				else // 있던것
				{
					int eventID = excelList.get(i).getAppEventID();
					for(int j=0;j<eventList.size();j++)
					{
						if(eventList.get(j).getAppEventID() == eventID)
						{
							if(excelList.get(i).getAppEventLimit() < excelList.get(i).getAppEventCount()  && excelList.get(i).getAppEventLimit()!=0)
							{
								dataSourceTransactionManager.rollback(transactionStatus);
								LOG.info("getAppEventLimit - limit count");
								return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_LIMIT_EXCEL,HttpStatus.BAD_REQUEST);
							}
							if(excelList.get(i).getAppEventStartTime().getTime() > excelList.get(i).getAppEventEndTime().getTime())
							{
								dataSourceTransactionManager.rollback(transactionStatus);
								LOG.info("getAppEventLimit - start time > end time");
								return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_START_EXCEL,HttpStatus.BAD_REQUEST);
							}
							AppEventDTO appEventDTO = eventList.get(j);
							appEventDTO.setAppID(appID);
							appEventDTO.setAppEventID(eventID);
							String enableStr = excelList.get(i).getAppEventEnable();
							boolean enable = false;
							if(enableStr.compareTo("O")==0)
							{
								enable = true;
							}
							String oneOffStr = excelList.get(i).getAppEventOffsets();
							boolean enableOneoff = false;
							if(oneOffStr.compareTo("O")==0)
							{
								enableOneoff = true;
							}
							appEventDTO.setAppEventEnable(enable);
							appEventDTO.setAppEventKey(excelList.get(i).getAppEventKey());
							appEventDTO.setAppEventContent(excelList.get(i).getAppEventContent());
							appEventDTO.setAppEventCoin(excelList.get(i).getAppEventCoin());
							appEventDTO.setAppEventStartTime(Timestamp.valueOf(sdf.format(excelList.get(i).getAppEventStartTime())+":00"));
							appEventDTO.setAppEventEndTime(Timestamp.valueOf(sdf.format(excelList.get(i).getAppEventEndTime())+":00"));
							appEventDTO.setAppEventLimit(excelList.get(i).getAppEventLimit());
							appEventDTO.setAppEventOneoff(enableOneoff);
							if(apiService.modifyAppEventByExcel(appEventDTO)==false)
							{
								dataSourceTransactionManager.rollback(transactionStatus);
								LOG.info("modifyAppEventByExcel - error");
								return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_REGIST_FAIL_EXCEL,HttpStatus.BAD_REQUEST);
							}
						}
					}
				}
			}
			dataSourceTransactionManager.commit(transactionStatus);
			return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_REGIST_SECCESS,HttpStatus.OK);
		} catch (Exception e) {
			e.printStackTrace();
			LOG.info(e.getMessage());
			dataSourceTransactionManager.rollback(transactionStatus);
			return new ResponseEntity<>(GranConfig.RETURN_APP_EVENT_REGIST_FAIL_EXCEL,HttpStatus.BAD_REQUEST);
		}
    }
}
