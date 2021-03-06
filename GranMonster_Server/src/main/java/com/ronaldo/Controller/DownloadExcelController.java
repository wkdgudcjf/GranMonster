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
    	model.addAttribute(ExcelConfig.HEAD,Arrays.asList("?????? ID(??????X) / ????????? 0?????? ??????","????????????(O/X)","????????????","??????","????????????(??????)","?????? ??????","?????? ??????","?????? ?????????(??????X)","?????? ?????????(??????)","????????? ??????"));
    	model.addAttribute(ExcelConfig.BODY,appEventList);
    }
}
