using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace DNA_Error_Fix_UI
{
    class DNAFixUtil
    {
        public const int TYPE_FTDNA = 0;
        public const int TYPE_23ANDME = 1;
        public const int TYPE_ANCESTRY = 2;
        public const int TYPE_DECODEME = 3;

        public static Hashtable map = new Hashtable();
        public static SortedDictionary<long, string>[] rsid_pos_map = new SortedDictionary<long, string>[23];
        public static Hashtable fixed_snps = new Hashtable();

        static bool use_one_allele_23andme = false;

        public static bool male=false;

        public static void MasterSNPlist(string[] files)
        {
            map.Clear();
            use_one_allele_23andme = false;
            for (int i = 0; i < 23; i++)
            {
                if (rsid_pos_map[i] == null)
                    rsid_pos_map[i] = new SortedDictionary<long, string>();
                rsid_pos_map[i].Clear();
            }
            fixed_snps.Clear();
            foreach (string file in files)
            {
                updateMasterSNPlist(file, map);
            }
        }

        public static void fixSNPlist(string[] files)
        {
            //fix all
            foreach (string file in files)
            {
                fixFileSNPlist(file, map);
            }
        }

        private static void fixFileSNPlist(string file, Hashtable map)
        {
            
            StreamWriter fw = new StreamWriter(file+".fixed");
            
            string[] lines = File.ReadAllLines(file);

            int type = detectDNAFileType(lines);
            string[] data = null;
            string tLine = null;
            string key = null;
            string value = null;
            string allele1 = null;
            string allele2 = null;
            string snp = null;
            long pos = 0;
            int chr = 0;
            foreach (string line in lines)
            {
                //
                if (type == TYPE_FTDNA)
                {
                    if (line.StartsWith("RSID"))
                    {
                        fw.WriteLine(line);
                        continue;
                    }
                    if (line.Trim() == "")
                        continue;
                    //
                    tLine = line.Replace("\"", "");
                    data = tLine.Split(",".ToCharArray());
                    key = data[0] + ":" + data[1];
                    value = data[3];
                    if (data[1] == "X")
                        chr = 23;
                    else if (!(data[1] == "Y" || data[1] == "MT" || data[1] == "M" || data[1] == "24" || data[1] == "25" || data[1] == "0"))
                        chr = int.Parse(data[1]);
                    if(chr==23 && male)
                        snp = fixSNP_X(key, value, map, file);
                    else
                        snp = fixSNP(key, value, map, file);
                    if (snp == Reverse(value) && snp!=value)
                        snp = value;
                    fw.WriteLine("\"" + data[0] + "\",\"" + data[1] + "\",\"" + data[2] + "\",\"" + snp + "\"");

                    if (!(data[1] == "Y" || data[1] == "MT" || data[1] == "M" || data[1] == "24" || data[1] == "25" || data[1] == "0"))
                    {
                        pos = long.Parse(data[2]);
                        if (rsid_pos_map[chr - 1] == null)
                            rsid_pos_map[chr - 1] = new SortedDictionary<long, string>();
                        if (!rsid_pos_map[chr - 1].ContainsKey(pos))
                            rsid_pos_map[chr - 1].Add(pos, key + ":" + snp);
                    }
                }
                else if (type == TYPE_23ANDME)
                {
                    if (line.StartsWith("#"))
                    {
                        fw.WriteLine(line);
                        continue;
                    }
                    if (line.Trim() == "")
                        continue;
                    //       
                    data = line.Split("\t".ToCharArray());
                    key = data[0] + ":" + data[1];
                    value = data[3];
                    if (data[1] == "X")
                        chr = 23;
                    else if (!(data[1] == "Y" || data[1] == "MT" || data[1] == "M" || data[1] == "24" || data[1] == "25"))
                        chr = int.Parse(data[1]);

                    if (chr == 23 && male)
                        snp = fixSNP_X(key, value, map, file);
                    else
                        snp = fixSNP(key, value, map, file);
                    if (snp == Reverse(value) && snp != value)
                        snp = value;
                    if (use_one_allele_23andme && data[1]=="X") // means male and only one allele must exist
                    {
                            fw.WriteLine(data[0] + "\t" + data[1] + "\t" + data[2] + "\t" + snp[0].ToString());
                    }
                    else
                        fw.WriteLine(data[0] + "\t" + data[1] + "\t" + data[2] + "\t" + snp);
                    //
                    if (!(data[1] == "Y" || data[1] == "MT" || data[1] == "M" || data[1] == "24" || data[1] == "25"))
                    {
                        pos = long.Parse(data[2]);
                        if (rsid_pos_map[chr - 1] == null)
                            rsid_pos_map[chr - 1] = new SortedDictionary<long, string>();
                        if (!rsid_pos_map[chr - 1].ContainsKey(pos))
                            rsid_pos_map[chr - 1].Add(pos, key + ":" + snp);
                    }
                }
                else if (type == TYPE_ANCESTRY)
                {
                    if (line.StartsWith("#"))
                    {
                        fw.WriteLine(line);
                        continue;
                    }
                    if (line.StartsWith("rsid\t"))
                    {
                        fw.WriteLine(line);
                        continue;
                    }
                    if (line.Trim() == "")
                        continue;
                    //
                    data = line.Split("\t".ToCharArray());
                    if (data[1] == "23")
                        key = data[0] + ":X";
                    else
                        key = data[0] + ":" + data[1];
                    value = data[3] + data[4];

                    if (data[1] == "X")
                        chr = 23;
                    else if (!(data[1] == "Y" || data[1] == "MT" || data[1] == "M" || data[1] == "24" || data[1] == "25"))
                        chr = int.Parse(data[1]);

                    if (chr == 23 && male)
                        tLine = fixSNP_X(key, value, map, file);
                    else
                        tLine = fixSNP(key, value, map, file);

                    allele1 = tLine[0] + "";
                    allele2 = tLine[1] + "";
                    snp = allele1 + allele2;
                    if (snp == Reverse(value) && snp != value)
                    {
                        snp = value;
                        allele1 = tLine[1] + "";
                        allele2 = tLine[0] + ""; 
                    }
                    fw.WriteLine(data[0] + "\t" + data[1] + "\t" + data[2] + "\t" + allele1 + "\t" + allele2);
                    //
                    if (!(data[1] == "Y" || data[1] == "MT" || data[1] == "M" || data[1] == "24" || data[1] == "25"))
                    {
                        pos = long.Parse(data[2]);                        
                        if (rsid_pos_map[chr - 1] == null)
                            rsid_pos_map[chr - 1] = new SortedDictionary<long, string>();
                        if (!rsid_pos_map[chr - 1].ContainsKey(pos))
                            rsid_pos_map[chr - 1].Add(pos, key + ":" + snp);
                    }
                }
                else if (type == TYPE_DECODEME)
                {
                    if (line.StartsWith("Name,"))
                    {
                        fw.WriteLine(line);
                        continue;
                    }
                    if (line.Trim() == "")
                        continue;
                    //
                    data = line.Split(",".ToCharArray());
                    key = data[0] + ":" + data[2];
                    value = data[5];
                    if (data[2] == "X")
                        chr = 23;
                    else if (!(data[2] == "Y" || data[2] == "MT" || data[2] == "M" || data[2] == "24" || data[2] == "25"))
                        chr = int.Parse(data[2]);

                    if (chr == 23 && male)
                        snp = fixSNP_X(key, value, map, file);
                    else
                        snp = fixSNP(key, value, map, file);
                    if (snp == Reverse(value) && snp != value)
                        snp = value;
                    fw.WriteLine(data[0] + "," + data[1] + "," + data[2] + "," + data[3] + "," + data[4] + "," + snp);

                    if (!(data[2] == "Y" || data[2] == "MT" || data[2] == "M" || data[2] == "24" || data[2] == "25"))
                    {
                        pos = long.Parse(data[3]);
                        if (rsid_pos_map[chr - 1] == null)
                            rsid_pos_map[chr - 1] = new SortedDictionary<long, string>();
                        if (!rsid_pos_map[chr - 1].ContainsKey(pos))
                            rsid_pos_map[chr - 1].Add(pos, key + ":" + snp);
                    }
                }
            }
            fw.Close();
        }

        private static string fixSNP_X(string key, string value, Hashtable map, String file)
        {
            // for males and X only
            String file_only = Path.GetFileName(file);
            ArrayList value_array = (ArrayList)map[key];
            if (value_array == null)
                return value;
            bool is_error = false;
            foreach (string v in value_array)
            {
                if (v != value)
                    is_error = true;
            }
            if (!is_error)
                return value;

            Hashtable snps = new Hashtable();
            snps.Add("AA", 0);
            snps.Add("TT", 0);
            snps.Add("GG", 0);
            snps.Add("CC", 0);

            int count = 0;
            foreach (string v in value_array)
            {
                if (snps.ContainsKey(v))
                {
                    count = (int)snps[v];
                    snps.Remove(v);
                    count++;
                    snps.Add(v, count);
                }
                else if (snps.ContainsKey(Reverse(v)))
                {
                    count = (int)snps[Reverse(v)];
                    snps.Remove(Reverse(v));
                    count++;
                    snps.Add(Reverse(v), count);
                }
                else if (snps.ContainsKey(v[0] + v[0]))
                {
                    count = (int)snps[v[0] + v[0]];
                    snps.Remove(v[0] + v[0]);
                    count++;
                    snps.Add(v[0] + v[0], count);
                }
                else if (snps.ContainsKey(v[1] + v[1]))
                {
                    count = (int)snps[v[1] + v[1]];
                    snps.Remove(v[1] + v[1]);
                    count++;
                    snps.Add(v[1] + v[1], count);
                }
                else
                {
                    // no call - dont count
                }
            }
            string snp = null;
            int max = 0;
            foreach (string k in snps.Keys)
            {
                if (((int)snps[k]) > max)
                {
                    max = (int)snps[k];
                    snp = k;
                }
            }
            if (snp == null)
                snp = value;

            if (snp != value)
            {
                ArrayList list = new ArrayList();
                if (fixed_snps.ContainsKey(key))
                    list = (ArrayList)fixed_snps[key];
                fixed_snps.Remove(key);
                list.Add(file_only + ":" + snp);
                fixed_snps.Add(key, list);
            }
            return snp;
        }

        private static string fixSNP(string key, string value, Hashtable map, String file)
        {
            String file_only = Path.GetFileName(file);
            ArrayList value_array = (ArrayList)map[key];
            if (value_array == null)
                return value;
            bool is_error = false;
            foreach (string v in value_array)
            {
                if (v != value)
                    is_error = true;
            }
            if (!is_error)
                return value;

            Hashtable snps = new Hashtable();
            snps.Add("AA", 0);
            snps.Add("AT", 0);
            snps.Add("AG", 0);
            snps.Add("AC", 0);
            snps.Add("TT", 0);
            snps.Add("GT", 0);
            snps.Add("CT", 0);
            snps.Add("GG", 0);
            snps.Add("CG", 0);
            snps.Add("CC", 0);

            int count=0;
            foreach(string v in value_array)
            {
                if (snps.ContainsKey(v))
                {
                    count = (int)snps[v];
                    snps.Remove(v);
                    count++;
                    snps.Add(v, count);
                }
                else if (snps.ContainsKey(Reverse(v)))
                {
                    count = (int)snps[Reverse(v)];
                    snps.Remove(Reverse(v));
                    count++;
                    snps.Add(Reverse(v), count);
                }
                else
                {
                    // no call - dont count
                }
            }
            string snp = null;
            int max = 0;
            foreach (string k in snps.Keys)
            {
                if (((int)snps[k]) > max)
                {
                    max = (int)snps[k];
                    snp = k;
                }
            }
            if (snp == null)
                snp = value;
            
            if (snp != value)
            {
                ArrayList list=new ArrayList();
                if(fixed_snps.ContainsKey(key))
                    list = (ArrayList)fixed_snps[key];
                fixed_snps.Remove(key);
                list.Add(file_only + ":" + snp);
                fixed_snps.Add(key, list);
            }
            return snp;
        }
        
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private static void updateMasterSNPlist(string file, Hashtable map)
        {
            string[] lines=File.ReadAllLines(file);

            int type = detectDNAFileType(lines);
            if (type == -1)
            {
                Console.WriteLine("Unable to identify file format for "+file);
                Environment.Exit(0);
            }
            string[] data = null;
            string tLine=null;
            string key = null;
            string value = null;           

            ArrayList value_array = null;
            foreach (string line in lines)
            {
                //
                if (type == TYPE_FTDNA)
                {
                    if (line.StartsWith("RSID"))
                        continue;
                    if (line.Trim() == "")
                        continue;
                    //
                    tLine = line.Replace("\"", "");
                    data = tLine.Split(",".ToCharArray());                         
                    key=data[0] + ":" + data[1];
                    value=data[3];
                    value=value.Replace("D","-");

                    if (data[1] == "Y" || data[1] == "MT" || data[1] == "M" || data[1] == "24" || data[1] == "25" || data[1] == "0")
                        continue;

                }
                if (type == TYPE_23ANDME)
                {
                    if (line.StartsWith("#"))
                        continue;
                    if (line.Trim() == "")
                        continue;
                    //       
                    data = line.Split("\t".ToCharArray());
                    key = data[0] + ":" + data[1];
                    value = data[3];

                    if (data[1] == "Y" || data[1] == "MT" || data[1] == "M" || data[1] == "24" || data[1] == "25")
                        continue;
                    if (data[1] == "X")
                    {
                        if (value.Length == 1)
                        {
                            use_one_allele_23andme = true;// to reproduce the exact file in export
                            value = value + value;// X sometimes have only 1 allele (for males).
                        }
                    }
                }
                if (type == TYPE_ANCESTRY)
                {
                    if (line.StartsWith("#"))
                        continue;
                    if (line.StartsWith("rsid\t"))
                        continue;
                    if (line.Trim() == "")
                        continue;
                    //            
                    data = line.Split("\t".ToCharArray());
                    if (data[1] == "Y" || data[1] == "MT" || data[1] == "M" || data[1] == "24" || data[1] == "25")
                        continue;
                    if(data[1]=="23")
                        key = data[0] + ":X";
                    else
                        key = data[0] + ":" + data[1];
                    value = data[3]+data[4];
                    value=value.Replace("0","-");
                }
                if (type == TYPE_DECODEME)
                {
                    if (line.StartsWith("Name,"))
                        continue;
                    if (line.Trim() == "")
                        continue;
                    //            
                    data = line.Split(",".ToCharArray());
                    //
                    key = data[0] + ":" + data[2];
                    value = data[5];

                    if (data[2] == "Y" || data[2] == "MT" || data[2] == "M" || data[2] == "24" || data[2] == "25")
                        continue;
                }
                if (!map.ContainsKey(key))
                {
                    value_array = new ArrayList();                    
                }
                else
                {
                    value_array = (ArrayList)map[key];
                }
                value_array.Add(value);
                map.Remove(key);
                map.Add(key, value_array);
            }
        }

        private static int detectDNAFileType(string[] lines)
        {
            int count = 0;
            foreach (string line in lines)
            {
                if (line == "RSID,CHROMOSOME,POSITION,RESULT")
                    return TYPE_FTDNA;
                if (line == "# rsid\tchromosome\tposition\tgenotype")
                    return TYPE_23ANDME;
                if (line == "rsid\tchromosome\tposition\tallele1\tallele2")
                    return TYPE_ANCESTRY;
                if (line == "Name,Variation,Chromosome,Position,Strand,YourCode")
                    return TYPE_DECODEME;
                if (line.Split("\t".ToCharArray()).Length == 4)
                    return TYPE_23ANDME;
                if (line.Split("\t".ToCharArray()).Length == 5)
                    return TYPE_ANCESTRY;
                if (line.Split(",".ToCharArray()).Length == 4)
                    return TYPE_FTDNA;
                if (line.Split(",".ToCharArray()).Length == 6)
                    return TYPE_DECODEME;
                if (count > 100)
                {
                    // detection useless... 
                    break;
                }
                count++;
            }
            return -1;
        }

        public static string getConsolidatedFile(int type)
        {
            StringBuilder sb = new StringBuilder();
            Hashtable map_rm = new Hashtable(map);
            switch (type)
            {
                case TYPE_FTDNA:
                    sb.Append("RSID,CHROMOSOME,POSITION,RESULT");
                    break;
                case TYPE_23ANDME:
                    sb.Append("# rsid\tchromosome\tposition\tgenotype");
                    break;
                case TYPE_ANCESTRY:
                    sb.Append("rsid\tchromosome\tposition\tallele1\tallele2");
                    break;
                default:
                    break;
            }
            sb.Append("\r\n");     
            string[] rsid_chr_snp = null;
            for (int i = 0; i < 23; i++)
            {
                if (rsid_pos_map[i] == null)
                    continue;
                //
                string key = null;
                foreach(KeyValuePair<long,string> kv in rsid_pos_map[i])
                {              
                    rsid_chr_snp=kv.Value.Split(":".ToCharArray());// rsid:chr_snp
                    key = rsid_chr_snp[0] + ":" + rsid_chr_snp[1];

                    if (!map_rm.ContainsKey(key)) //duplicate from another build another file
                        continue;

                    if (rsid_chr_snp[2] == "00" || rsid_chr_snp[2] == "--" || rsid_chr_snp[2] == "II" || rsid_chr_snp[2] == "DD" || rsid_chr_snp[2] == "??")
                        continue;

                    switch (type)
                    {
                        case TYPE_FTDNA:
                            sb.Append("\"");
                            sb.Append(rsid_chr_snp[0]);
                            sb.Append("\",\"");
                            sb.Append(rsid_chr_snp[1]);
                            sb.Append("\",\"");
                            sb.Append(kv.Key.ToString());
                            sb.Append("\",\"");
                            if (rsid_chr_snp[2].Length == 1 && (rsid_chr_snp[1] == "X" || rsid_chr_snp[1] == "23"))
                                sb.Append(rsid_chr_snp[2]);
                            else
                                sb.Append(rsid_chr_snp[2]);                            
                            sb.Append("\"");
                            break;
                        case TYPE_23ANDME:
                            sb.Append(rsid_chr_snp[0]);
                            sb.Append("\t");
                            sb.Append(rsid_chr_snp[1]);
                            sb.Append("\t");
                            sb.Append(kv.Key.ToString());
                            sb.Append("\t");
                            if(use_one_allele_23andme && rsid_chr_snp[1]=="X")
                                sb.Append(rsid_chr_snp[2][0].ToString());
                            else
                                sb.Append(rsid_chr_snp[2]);
                            break;
                        case TYPE_ANCESTRY:
                            sb.Append(rsid_chr_snp[0]);
                            sb.Append("\t");
                            if(rsid_chr_snp[1]=="X")
                                sb.Append("23");
                            else
                                sb.Append(rsid_chr_snp[1]);
                            sb.Append("\t");
                            sb.Append(kv.Key.ToString());
                            sb.Append("\t");
                            sb.Append(rsid_chr_snp[2][0].ToString());
                            sb.Append("\t");
                            if (rsid_chr_snp[2].Length == 1 && (rsid_chr_snp[1] == "X"||rsid_chr_snp[1]=="23"))
                                sb.Append(rsid_chr_snp[2][0].ToString());
                            else
                                sb.Append(rsid_chr_snp[2][1].ToString());
                            break;
                        default:
                            break;
                    }
                    sb.Append("\r\n");
                    map_rm.Remove(key);
                }
            }
            return sb.ToString();
        }
    }
}
