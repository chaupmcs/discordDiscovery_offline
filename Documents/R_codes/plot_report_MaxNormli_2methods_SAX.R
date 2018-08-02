##### report for Discord_Timeseries project

# Ver 2.4: Jan 2nd 2017 |
#   =====================
#     'BitClustering Li_Braysy': K_Clusters = 28, W_length = N_length / 4;
#     'HOT_SAX + Squeezer' and 'BitCluster + Squeezer': threshold = 0.85, W_length=5;
#     'HOT_SAX': W_length = 3;
#     
#     *Note: 
#       time unit is millisecond.
#     
#     TEK16: 	(N_length: "128")
#     BitClustering Li_Braysy: 1426, 1336, 2033, 1150, 1225
#     BitCluster + Squeezer: 973, 1029, 1040, 1253, 1252
#     HOT_SAX: 5362, 5406, 5407, 5327, 5316
#     HOT_SAX + Squeezers: 1730, 1857, 1755, 1716, 1783
#     
#     
#     TEK17: (N_length: "128")
#     BitClustering Li_Braysy: 1526, 2247, 2473, 1565, 1884
#     BitCluster + Squeezer: 1754, 2083, 1951, 1912, 1940
#     HOT_SAX: 6229, 6118, 6037, 6135, 6239
#     HOT_SAX + Squeezers: 1049, 1048, 1021, 1141, 1197
#     
#     
#     ECG: (N_length: "40")
#     BitClustering Li_Braysy: 4467, 4996, 4324, 4097, 4144
#     BitCluster + Squeezer: 4857, 5091, 4707, 4658, 4992
#     HOT_SAX: 37902, 37886, 37620, 37373, 38130
#     HOT_SAX + Squeezers: 18618, 18478, 18754, 18569, 18824
#     
#     power_data: (N_length: "200")
#     BitClustering Li_Braysy: 23651, 27904, 28663, 27417, 23799
#     BitCluster + Squeezer: 22266, 26483, 24031, 24217, 22067
#     HOT_SAX: 215822, 256714, 214682, 210732, 226393
#     HOT_SAX + Squeezers: 26194, 26168, 26389, 26837, 26265
#     
#     nprs43: (N_length: "160")
#     BitClustering Li_Braysy: 14208, 9993, 10956, 12452, 10698
#     BitCluster + Squeezer: 9685, 9754, 10037, 10736, 10424
#     HOT_SAX: 45242, 46140, 46584, 45803, 47454
#     HOT_SAX + Squeezers: 18920, 18429, 18420, 17727, 17950
#     
#     nprs44: (N_length: "160")
#     BitClustering Li_Braysy: 11029, 13268, 11682, 12679, 10901
#     BitCluster + Squeezer: 10532, 11454, 11210, 10736, 10393
#     HOT_SAX: 73173, 72792, 74051, 72907, 72529
#     HOT_SAX + Squeezers: 37491, 38156, 37665, 37594, 36328
#     
#     ERP: (N_length: "64")
#     BitClustering Li_Braysy: 423232, 478050, 438785, 425828, 443904
#     BitCluster + Squeezer: 404934, 385386, 393415, 403835, 399607
#     HOT_SAX: 5582402
#     HOT_SAX + Squeezers: 866882
# 
normalize_max <- function(vector_x)
{
  min = min(vector_x)
  max = max(vector_x)
  interval = max - min

  vector_norm = sapply(vector_x, function(x){x=x/max})

  return(vector_norm)
}


HOTSAX            = c( mean(5362, 5406, 5407, 5327, 5316)/1000,
                       mean(6229, 6118, 6037, 6135, 6239)/1000,
                       mean(37902, 37886, 37620, 37373, 38130)/1000,
                       mean(215822, 256714, 214682, 210732, 226393)/1000,
                       mean(45242, 46140, 46584, 45803, 47454)/1000,
                       mean(73173, 72792, 74051, 72907, 72529)/1000,
                       mean(5582402)/1000 )

HOTSAX_Squeezer = c( mean(1730, 1857, 1755, 1716, 1783)/1000,
                     mean(1049, 1048, 1021, 1141, 1197)/1000,
                     mean(18618, 18478, 18754, 18569, 18824)/1000,
                     mean(26194, 26168, 26389, 26837, 26265)/1000,
                     mean(18920, 18429, 18420, 17727, 17950)/1000,
                     mean(37491, 38156, 37665, 37594, 36328)/1000,
                     mean(866882)/1000 )

report_raw = as.data.frame(cbind(HOTSAX, HOTSAX_Squeezer))
rownames(report_raw) = c("TEK16", "TEK17", "ECG", "power_data", "nprs43", "nprs44", "ERP") 
# View(report_raw)

# # Writing to file...
# file_link = paste0(dirname(getwd()), "/Reports","/report_time_execution.csv")
# write.csv(report_raw, file_link, row.names = T)

#norm size:
report = t(apply(report_raw, 1, normalize_max))

#data_names
data_names <-rep(rownames(report), each = ncol(report))

#method_names
method_names <- gl(ncol(report), 1, ncol(report)*nrow(report), labels=colnames(report))

#values
time_execution <-  as.vector(t(report))


data_plot <- data.frame(column1=data_names, column2=method_names, column3=time_execution)

require(ggplot2)
ggplot(data_plot, aes(x=data_names, y=time_execution, fill=method_names)) +  geom_bar(position=position_dodge(), stat="identity")

# #save the picture:
# link_store_picture = paste0(dirname(getwd()), "/Reports")
# ggsave("Report_BarChart_full.png",path = link_store_picture, scale = 2)