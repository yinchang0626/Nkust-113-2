using System;

namespace ConsoleApp1.Models
{
    public class RescueData
    {
        public int Month { get; set; }  // 月份
        public int GeneralAmbulance { get; set; }  // 一般救護車
        public int ICUAmbulance { get; set; }  // 加護救護車
        public int Transported { get; set; }  // 送醫次數
        public int NotTransported { get; set; }  // 未送醫次數
        public int AcuteDisease { get; set; }  // 急病
        public int DrugPoisoning { get; set; }  // 疑似藥物中毒
        public int CO_Poisoning { get; set; }  // 疑似一氧化碳中毒
        public int Seizure { get; set; }  // 癲癇或抽搐
        public int Collapse { get; set; }  // 路倒
        public int MentalDisorder { get; set; }  // 行為急症或精神異常
        public int PregnancyEmergency { get; set; }  // 孕婦急產
        public int NonTrauma_OHCA { get; set; }  // 非創傷類_OHCA
        public int NonTrauma_Other { get; set; }  // 非創傷類_其他
        public int GeneralTrauma { get; set; }  // 一般外傷
        public int TrafficInjury { get; set; }  // 車禍受傷
        public int Drowning { get; set; }  // 溺水
        public int FallInjury { get; set; }  // 摔跌傷
        public int Falling { get; set; }  // 墜落傷
        public int StabWound { get; set; }  // 穿刺傷
        public int Burn { get; set; }  // 燒燙傷
        public int ElectricShock { get; set; }  // 電擊傷
        public int AnimalBite { get; set; }  // 生物咬螫傷
        public int Trauma_OHCA { get; set; }  // 創傷類_OHCA
        public int Trauma_Other { get; set; }  // 創傷類_其他
    }
}
