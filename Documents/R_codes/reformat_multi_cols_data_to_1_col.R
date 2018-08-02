
#get link the input file
link_inputfile =  "C:\\Users\\chauuser_w10\\Desktop\\GIT_REPOS\\Discord_Timeseries\\BitClustering\\bin\\Debug\\Data\\chfdb_chf13_45590.txt"

#get link and name the output file

link_outputfile = "C:\\Users\\chauuser_w10\\Desktop\\GIT_REPOS\\Discord_Timeseries\\BitClustering\\bin\\Debug\\Data\\chfdb_chf13_275_1col.txt"

#read file to R
input_data = read.csv(link_inputfile, sep="", header = F)

#check input_data
#View(input_data)

#convert the input_data to just 1 col
data_one_col =as.vector(t(input_data))

#check
View(data.frame(data_one_col))

#write to file
write.csv(data_one_col, link_outputfile, row.names=F)

plot(1:500,data_one_col[1:500], type='o')

