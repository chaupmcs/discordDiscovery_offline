
#get link the input file
link_input_file = ("C:\\Users\\chauuser_w10\\Desktop\\GIT_REPOS\\Discord_Timeseries\\BitClustering\\bin\\Debug\\Data\\xmitdb_x108_0.txt")

#get link and name the output file
link_output_chfdb = ("C:\\Users\\chauuser_w10\\Desktop\\GIT_REPOS\\Discord_Timeseries\\BitClustering\\bin\\Debug\\Data\\xmitdb.txt")
link_output_chf01 = ("C:\\Users\\chauuser_w10\\Desktop\\GIT_REPOS\\Discord_Timeseries\\BitClustering\\bin\\Debug\\Data\\x108.txt")

#read file to R
input_data = read.csv(link_input_file, sep="", header = F)

data_chfdb= input_data[2]
data_chf01 = input_data[3]

write.csv(data_chfdb, link_output_chfdb, row.names=F)
write.csv(data_chf01, link_output_chf01, row.names=F)


plot(data_chfdb[,], type="o")
plot(data_chf01[,], type="o", col="blue")

# 
# link_erp = "C:\\Users\\chauuser_w10\\Desktop\\GIT_REPOS\\Discord_Timeseries\\BitClustering\\bin\\Debug\\Data\\ERP_1_col.csv"
# 
# data_erp = read.csv(link_erp, sep="", header = F)
# plot(1000:10000, data_erp[1000:10000,1], type="l")  
