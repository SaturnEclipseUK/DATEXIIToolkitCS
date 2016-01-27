curl -vX POST -d @"..\Messages\VMS_and_Matrix_Signal_Status_Data_-_Full_Refresh_3793697241479015.xml" http://localhost:49519/subscriber/submit

curl -vX POST -d @"..\Messages\ANPR_Journey_Time_Data_3794915029662987.xml" http://localhost:49519/subscriber/submit

curl -vX POST -d @"..\Messages\Event_Data_-_Full_Refresh_3794850927130403.xml" http://localhost:49519/subscriber/submit

curl -vX POST -d @"..\Messages\Fused_FVD_and_Sensor_PTD_3793821203699073.xml" http://localhost:49519/subscriber/submit

curl -vX POST -d @"..\Messages\Fused_Sensor-only_PTD_3793817774626104.xml" http://localhost:49519/subscriber/submit

curl -vX POST -d @"..\Messages\MIDAS_Loop_Traffic_Data_3793828281116175.xml" http://localhost:49519/subscriber/submit

curl -vX POST -d @"..\Messages\TMU_Loop_Traffic_Data_3793809993027605.xml" http://localhost:49519/subscriber/submit
