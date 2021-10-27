package com.ronaldo.config;

import java.io.IOException;
import java.sql.Timestamp;
import java.text.SimpleDateFormat;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonSerializer;
import com.fasterxml.jackson.databind.SerializerProvider;

public class Write_Formatting extends JsonSerializer<Timestamp> {
	@Override
	public void serialize(Timestamp comment_date, com.fasterxml.jackson.core.JsonGenerator gen, SerializerProvider arg2)
			throws IOException, JsonProcessingException {
		SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm");
	    String formattedDate = formatter.format(comment_date);
	    gen.writeString(formattedDate);
	}
}