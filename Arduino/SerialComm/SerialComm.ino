int buttonState=0;
void setup()
{

  /* add setup code here */
	/* Serial Read Operation */
	 /* pinMode(13,OUTPUT);
	Serial.begin(9600);*/
	
	/* Serial Write Operation */
	//pinMode(7,INPUT);
	//digitalWrite(7,HIGH);
	pinMode(13,OUTPUT);
	Serial.begin(9600);
}

void loop()
{
   /* Serial Write Operation */
	if(Serial.available())
	{
	  int c=Serial.read();
	  if(c=='1')
	  {
      //buttonState=digitalRead(7);
	  
		  digitalWrite(13,HIGH);
		  Serial.println('A');
	  }
	  else if (c=='0')
	  {
        digitalWrite(13,LOW);
		Serial.println('B');
	  }
	}

  /* Serial Read Operation */
	/*
	if(Serial.available())
	{
		int c=Serial.read();
		if(c=='1')
		{
			digitalWrite(13,HIGH);
		}
		else if(c=='0')
		{
			digitalWrite(13,LOW);
		}
		
	}*/
	

}
