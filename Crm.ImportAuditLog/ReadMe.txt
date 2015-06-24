תוכנה ל AUDITLOG
התוכנה מעבירה את כל השינוים בן תאריכים 
הסבר קונפיגורציה

פרטי התחברות 
OrganizationServiceURL= ה CRM 
<add key="OrganizationServiceURL" value="http://moincrm13/Moin/XRMServices/2011/Organization.svc" />
שם משתמש
<add key="UserName" value="moinadmin" />
סיסמא
<add key="Password" value="pass@word1" />
דומיין
<add key="Domain" value="gdevdom" />
לא געת
<add key="UseDefaultCredentials" value="false" />
    
מאחר והמערכת דוגמת את הלוג השינויים של ה CRM 
אז עדיף שהתאריך האחרון שבוצע התהליך 
להוסיף לו דקות אחורה 
נניח והתהליך הסתיים לרוץ
ב 24-6-2015 בשעה 10:30
אז הפרמטר ערך 5 אומר לו תתחיל לרוץ מתאריך
24-6-2015 בשעה 10:25
<add key="RoundEndDateMintuesValue" value="5" /><!-- minute before-->
במידה ואן לוג תאריך - קרי התוכנה רצה חדשה 
אז רוץ מהימים האחרונים
<add key="LastXDays" value="1" />
עד תאריך 
ברירת מחדל אן ערך אז עד התאריך של עכשיו ב UTC
<add key="UntilDate" value="" />  <!--  empty =DateTime.Now  else exmple format 2015-06-22-->
לא לגעת
<add key="MaxRecordsPerExcution" value="4000" />
תרגום שמות השדות 
<add key="LanguageCode" value="1037" /> <!-- ENG =1033 ,HEB =1037-->
במידה ויש דופליקית ברשומות 
(דופליקציה היא פר GUID LOGCRM,FIELDSCHEMANAME,ENTITYNAME)
ערך 1 אומר מחק רשומות כפולות
<add key="IsRemoveDuplicate" value="1" /><!-- 1 =yes remove duplicate ,best practice other wise do not remove-->