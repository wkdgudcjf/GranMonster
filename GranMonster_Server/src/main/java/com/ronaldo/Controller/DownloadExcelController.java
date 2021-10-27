package com.ronaldo.Controller;

import com.ronaldo.Component.ExcelXlsView;
import com.ronaldo.Component.ExcelXlsxStreamingView;
import com.ronaldo.Component.ExcelXlsxView;
import com.ronaldo.config.ExcelConfig;
import com.ronaldo.domain.AppDTO;
import com.ronaldo.domain.AppEventDTO;
import com.ronaldo.service.ApiService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.servlet.View;

import java.util.Arrays;
import java.util.List;

@Controller
public class DownloadExcelController {
	
	@Autowired
	private ApiService apiService;
	
    @RequestMapping(value = "/excelDownload-xls", method = RequestMethod.POST)
    public View xlsView(@RequestParam("appID") int appID,Model model) {
    	ExcelXlsView excelXlsView = new ExcelXlsView();
    	model.addAttribute("appID",appID);
    	getEventMap(model,appID);
    	return excelXlsView;
    }

    @RequestMapping(value = "/excelDownload-xlsx", method = RequestMethod.POST)
    public View xlsxView(@RequestParam("appID") int appID,Model model) {
    	ExcelXlsxView excelXlsxView = new ExcelXlsxView();
    	model.addAttribute("appID",appID);
    	getEventMap(model,appID);
    	return excelXlsxView;
    }

    @RequestMapping(value = "/excelDownload-xlsx-streaming", method = RequestMethod.POST)
    public View xlsxStreamingView(@RequestParam("appID") int appID,Model model) {
    	ExcelXlsxStreamingView excelXlsxStreamingView = new ExcelXlsxStreamingView();
    	model.addAttribute("appID",appID);
    	getEventMap(model,appID);
    	return excelXlsxStreamingView;
    }

    private void getEventMap(Model model,int appID) {
    	AppDTO appDTO = apiService.getApp(appID);
    	List<AppEventDTO> appEventList = apiService.getAppEventList(appID);
    	model.addAttribute(ExcelConfig.FILE_NAME, appDTO.getAppName()+"_event");
    	model.addAttribute(ExcelConfig.HEAD,Arrays.asList("고유 ID(변경X) / 추가는 0으로 셋팅","활성여부(O/X)","이벤트키","내용","보상코인(숫자)","시작 시간","종료 시간","달성 인원수(변경X)","제한 인원수(숫자)","일회성 여부"));
    	model.addAttribute(ExcelConfig.BODY,appEventList);
    }
}
