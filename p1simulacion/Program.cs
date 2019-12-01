using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p1simulacion {
	class Program {

		private static int[] generate() {
			var nums = new List<int>();
			for(int a = 0; a < 10; a++) {
				int numa = a;
				for(int b = 0; b < 10; b++) {
					if(b == a)
						continue;
					int numb = numa * 10 + b;
					for(int c = 0; c < 10; c++) {
						if(c == a || c == b)
							continue;
						int numc = numb * 10 + c;
						for(int d = 0; d < 10; d++) {
							if(d == a || d == b || d == c)
								continue;
							//Console.WriteLine(numc * 10 + d);
							nums.Add(numc * 10 + d);
						}
					}
				}
			}
			return nums.ToArray();
		}

		static int[] numeros = generate();

		// cuadrados, productos
		//static double[,] seedsNC = new double[100, 2];
		static double[][] seedsNC = new double[2][];
		// lineal, multiplicativo
		//static double[,] seedsC = new double[100, 2];
		static double[][] seedsC = new double[2][];

		static double U = 0.5;
		static double sqrt = Math.Sqrt(1200);
		static double[][] limitesX2 = {
			new double[] { 0.0649, 0.1036 },
			new double[] { 0.0619, 0.108 },
			new double[] { 0.0561, 0.1168 }
		};
		static double[][] limitesZ = {
			new double[] { U - (1.64 / sqrt), U + (1.64 / sqrt) },
			new double[] { U - (1.96 / sqrt), U + (1.96 / sqrt) },
			new double[] { U - (2.58 / sqrt), U + (2.58 / sqrt) }
		};

		// TD, 1P, 2P, T, P
		//static double[] poker4 = { 0.5040, 0.4320, 0.0270, 0.0360, 0, 0010 };
		static double[] pokerFE = { 50.4, 43.2, 2.7, 3.6, 0.1 };
		static double[] ksValue = { 1.22 / 99, 1.36 / 99, 1.63 / 99 };

		static Random RND = new Random();


		static double[] seriesX2 = { 24.9958, 27.4884, 32.8013 };

		static Dictionary<double, double> tableZ = new Dictionary<double, double>();

		private static void fillZ() {
			double[] auxKn = new double[310];
			double[] auxKp = new double[310];
			int i = 0;
			double j = 0.00;
			auxKn[0] = 0.00;
			while(i < 310) {
				auxKn[i] = double.Parse((-j).ToString("0.##"));
				auxKp[i] = double.Parse(j.ToString("0.##"));
				j += 0.01;
				i++;
			}
			double[] valuesn = {
				0.5000, 0.4960, 0.4920, 0.4880, 0.4840, 0.4801, 0.4761, 0.4721, 0.4681, 0.4641,
				0.4602, 0.4562, 0.4522, 0.4483, 0.4443, 0.4404, 0.4364, 0.4325, 0.4286, 0.4247,
				0.4207, 0.4168, 0.4129, 0.4090, 0.4052, 0.4013, 0.3974, 0.3936, 0.3897, 0.3859,
				0.3821, 0.3783, 0.3745, 0.3707, 0.3669, 0.3632, 0.3594, 0.3557, 0.3520, 0.3483,
				0.3446, 0.3409, 0.3372, 0.3336, 0.3300, 0.3264, 0.3228, 0.3192, 0.3156, 0.3121,
				0.3085, 0.3050, 0.3015, 0.2981, 0.2946, 0.2912, 0.2877, 0.2843, 0.2806, 0.2776,
				0.2743, 0.2709, 0.2676, 0.2643, 0.2611, 0.2578, 0.2546, 0.2514, 0.2483, 0.2451,
				0.2420, 0.2389, 0.2358, 0.2327, 0.2296, 0.2266, 0.2236, 0.2206, 0.2177, 0.2148,
				0.2119, 0.2090, 0.2061, 0.2033, 0.2005, 0.1977, 0.1949, 0.1922, 0.1894, 0.1867,
				0.1841, 0.1814, 0.1788, 0.1762, 0.1736, 0.1711, 0.1685, 0.1660, 0.1635, 0.1611,
				0.1587, 0.1562, 0.1539, 0.1515, 0.1492, 0.1469, 0.1446, 0.1423, 0.1401, 0.1379,
				0.1357, 0.1335, 0.1314, 0.1292, 0.1271, 0.1251, 0.1230, 0.1210, 0.1190, 0.1170,
				0.1151, 0.1131, 0.1112, 0.1093, 0.1075, 0.1056, 0.1038, 0.1020, 0.1003, 0.0985,
				0.0968, 0.0951, 0.0934, 0.0918, 0.0901, 0.0885, 0.0869, 0.0853, 0.0838, 0.0823,
				0.0808, 0.0793, 0.0778, 0.0764, 0.0749, 0.0735, 0.0721, 0.0708, 0.0694, 0.0681,
				0.0668, 0.0655, 0.0643, 0.0630, 0.0618, 0.0606, 0.0594, 0.0582, 0.0571, 0.0559,
				0.0548, 0.0537, 0.0526, 0.0516, 0.0505, 0.0495, 0.0485, 0.0475, 0.0465, 0.0455,
				0.0446, 0.0436, 0.0427, 0.0418, 0.0409, 0.0401, 0.0392, 0.0384, 0.0375, 0.0367,
				0.0359, 0.0351, 0.0344, 0.0336, 0.0329, 0.0322, 0.0314, 0.0307, 0.0301, 0.0294,
				0.0287, 0.0281, 0.0274, 0.0268, 0.0262, 0.0256, 0.0250, 0.0244, 0.0239, 0.0233,
				0.0228, 0.0222, 0.0217, 0.0212, 0.0207, 0.0202, 0.0197, 0.0192, 0.0188, 0.0183,
				0.0179, 0.0174, 0.0170, 0.0166, 0.0162, 0.0158, 0.0154, 0.0150, 0.0146, 0.0143,
				0.0139, 0.0136, 0.0132, 0.0129, 0.0125, 0.0122, 0.0119, 0.0116, 0.0113, 0.0110,
				0.0107, 0.0104, 0.0102, 0.0099, 0.0096, 0.0094, 0.0091, 0.0089, 0.0087, 0.0084,
				0.0082, 0.0080, 0.0078, 0.0075, 0.0073, 0.0071, 0.0069, 0.0068, 0.0066, 0.0064,
				0.0062, 0.0060, 0.0059, 0.0057, 0.0055, 0.0054, 0.0052, 0.0051, 0.0049, 0.0048,
				0.0047, 0.0045, 0.0044, 0.0043, 0.0041, 0.0040, 0.0039, 0.0038, 0.0037, 0.0036,
				0.0035, 0.0034, 0.0033, 0.0032, 0.0031, 0.0030, 0.0029, 0.0028, 0.0027, 0.0026,
				0.0026, 0.0025, 0.0024, 0.0023, 0.0023, 0.0022, 0.0021, 0.0021, 0.0020, 0.0019,
				0.0019, 0.0018, 0.0018, 0.0017, 0.0016, 0.0016, 0.0015, 0.0015, 0.0014, 0.0014,
				0.0013, 0.0010, 0.0007, 0.0005, 0.0003, 0.0002, 0.0001, 0.0011, 0.0001, 0.0000,
			};

			double[] valuesp = new double[valuesn.Length];
			for(i = 0; i < valuesn.Length; i++) {
				valuesp[i] = 1 - valuesn[i];
			}

			tableZ.Add(auxKn[0], valuesn[0]);
			for(i = 1; i < 310; i++) {
				tableZ.Add(auxKn[i], valuesn[i]);
				tableZ.Add(auxKp[i], valuesp[i]);
			}
		}

		private static int RandomNumber() {
			int rnd = RND.Next(numeros.Length);
			//Console.Write("rnd: " + rnd + "; ");
			return numeros[rnd];
		}

		private static double media(double[] n) {
			double x = 0;
			int l = n.Length;
			for(int i = 0; i < l; i++) {
				x += n[i];
			}
			return x / l;
		}

		private static double varianza(double[] n) {
			double v = 0;
			double x = media(n);
			int l = n.Length;
			for(int i = 0; i < l; i++) {
				double z = n[i] - x;
				v += z * z;
			}
			return v / l;
		}

		private static double[] operado(double[] n) {
			int l = n.Length;
			double fe = (l - 1) / 16;
			int[] o = new int[16];
			for(int i = 0; i < l - 1; i++) {
				var x = n[i];
				var y = n[i + 1];
				if(x > 0.25) {
					if(x > 0.5) {
						if(x > 0.75) {
							if(y > 0.25) {
								if(y > 0.5) {
									if(y > 0.75) {
										o[15]++;
									} else {
										o[14]++;
									}
								} else {
									o[13]++;
								}
							} else {
								o[12]++;
							}
						} else {
							if(y > 0.25) {
								if(y > 0.5) {
									if(y > 0.75) {
										o[11]++;
									} else {
										o[10]++;
									}
								} else {
									o[9]++;
								}
							} else {
								o[8]++;
							}
						}
					} else {
						if(y > 0.25) {
							if(y > 0.5) {
								if(y > 0.75) {
									o[7]++;
								} else {
									o[6]++;
								}
							} else {
								o[5]++;
							}
						} else {
							o[4]++;
						}
					}
				} else {
					if(y > 0.25) {
						if(y > 0.5) {
							if(y > 0.75) {
								o[3]++;
							} else {
								o[2]++;
							}
						} else {
							o[1]++;
						}
					} else {
						o[0]++;
					}
				}
			}
			double[] ff = new double[16];
			for(int i = 0; i < 16; i++) {
				ff[i] = ((fe - o[i]) * (fe - o[i])) / fe;
			}
			return ff;
		}

		private static string printArray(double[] arr) {
			string res = "\n[\n";
			int l = arr.Length;
			for(int i = 0; i < l - 1; i++) {
				res += "  " + arr[i] + ",\n";
				//res += "\t" +  + ",\n";
			}
			return res + "  " + arr[l - 1] + "\n]\n";
		}

		private static string printArray(int[] arr) {
			string res = "\n[\n";
			int l = arr.Length;
			for(int i = 0; i < l - 1; i++) {
				res += "  " + arr[i] + ",\n";
				//res += "\t" +  + ",\n";
			}
			return res + "  " + arr[l - 1] + "\n]\n";
		}

		private static double analizarPoker(double[] dd) {
			int[] fo = new int[5];
			for(int i = 0; i < dd.GetLength(0); i++) {
				int n = (int) (dd[i] * 10000);
				int[] c = new int[10];
				while(n > 0) {
					int m = n % 10;
					c[m]++;
					n /= 10;
				}
				Array.Sort(c);
				//Array.Reverse(c);
				string cs = string.Join("", c);
				if(cs.Contains("4")) {
					fo[4]++;
				} else if(cs.Contains("3")) {
					fo[3]++;
				} else if(cs.Contains("22")) {
					fo[2]++;
				} else if(cs.Contains("2")) {
					fo[1]++;
				} else {
					fo[0]++;
				}
			}

			double[] res = new double[5];
			for(int i = 0; i < 5; i++) {
				res[i] = ((pokerFE[i] - fo[i]) * (pokerFE[i] - fo[i])) / pokerFE[i];
			}
			double x = 0;
			for(int i = 0; i < 5; i++) {
				x += res[i];
			}
			return x;
		}

		private static void generateSeedNC() {
			seedsNC[0] = new double[100];
			seedsNC[1] = new double[100];
			for(int i = 0; i < 100; i++) {
				/*******No Congruentes*******/
				double n = RandomNumber();
				//Console.Write(n + " ");
				double nn = RandomNumber();
				n = Math.Pow(n, 2);
				//Console.Write(n + " ");
				nn = n * nn;
				string ns = n + "";
				string nns = nn + "";
				// abcdefgh
				if(ns.Length == 8) {
					ns = "0." + ns.Substring(2, 4);
				} else {
					ns = "0." + ns.Substring(1, 4);
				}
				//Console.Write(ns + " ");
				if(nns.Length == 8) {
					nns = "0." + nns.Substring(2, 4);
				} else {
					nns = "0." + nns.Substring(1, 4);
				}
				n = double.Parse(ns);
				nn = double.Parse(nns);
				//Console.WriteLine(n);
				//seedsNC[i, 0] = n;
				//seedsNC[i, 1] = nn;
				seedsNC[0][i] = n;
				seedsNC[1][i] = nn;
				/****************************/
			}

		}

		private static void generateSeedC() {
			int x0l = RND.Next(1, 10);
			int x0m = RND.Next(1, 20);
			int k = RND.Next(1, 10);
			int g = RND.Next(1, 9);
			int c = RND.Next(1, 10);
			c -= c % 2 == 1 ? 0 : 1;
			int ml = (int) Math.Pow(2, g);
			int mm = RND.Next(1, 100);
			int al = 1 + 4 * k;
			int am = RND.Next(1, 20);
			double xil = (al * x0l + c) % ml;
			double xim = (am * x0m) % mm;
			//Console.WriteLine(xil + " " + al + " " + x0l + " " + c + " " + ml + " " + g + " " + k);
			seedsC[0] = new double[100];
			seedsC[1] = new double[100];
			for(int i = 0; i < 100; i++) {
				/*********Congruentes********/
				//Console.WriteLine(xil + " " + (xil / (ml - 1)) + " " + seedsC[i, 0]);
				//seedsC[i, 0] = Math.Round(xil / (ml - 1), 4);
				//seedsC[i, 1] = Math.Round(xim / (mm - 1), 4);

				seedsC[0][i] = Math.Round(xil / (ml - 1), 4);
				seedsC[1][i] = Math.Round(xim / (mm - 1), 4);
				xil = (al * xil + c) % ml;
				xim = (am * xim) % mm;
				/****************************/
			}
		}

		private static void pruebaSMV() {
			double xc0 = media(seedsC[0]);
			double xc1 = media(seedsC[1]);
			double xnc0 = media(seedsNC[0]);
			double xnc1 = media(seedsNC[1]);
			double vc0 = varianza(seedsC[0]);
			double vc1 = varianza(seedsC[1]);
			double vnc0 = varianza(seedsNC[0]);
			double vnc1 = varianza(seedsNC[1]);
			double sc0 = operado(seedsC[0]).Sum();
			double sc1 = operado(seedsC[1]).Sum();
			double snc0 = operado(seedsNC[0]).Sum();
			double snc1 = operado(seedsNC[1]).Sum();

			switch(Console.ReadLine()) {
				case "a":
					double[] limm = limitesZ[0];
					double[] limv = limitesX2[0];
					bool[] ac = new bool[4];
					ac[0] = xnc0 > limm[0] && xnc0 < limm[1] && vnc0 > limv[0] && vnc0 < limv[1] && snc0 < seriesX2[0];
					ac[1] = xnc1 > limm[0] && xnc1 < limm[1] && vnc1 > limv[0] && vnc1 < limv[1] && snc1 < seriesX2[0];
					ac[2] = xc0 > limm[0] && xc0 < limm[1] && vc0 > limv[0] && vc0 < limv[1] && sc0 < seriesX2[0];
					ac[3] = xc1 > limm[0] && xc1 < limm[1] && vc1 > limv[0] && vc1 < limv[1] && sc1 < seriesX2[0];
					if(ac[0]) {
						Console.WriteLine("Se acepta Ho de la serie Cuadrados Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Cuadrados Medios");
					}
					if(ac[1]) {
						Console.WriteLine("Se acepta Ho de la serie Productos Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Productos Medios");
					}
					if(ac[2]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Lineal");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Lineal");
					}
					if(ac[3]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Multiplicativo");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Multiplicativo");
					}
					break;
				case "b":
					limm = limitesZ[1];
					limv = limitesX2[1];
					ac = new bool[4];
					ac[0] = xc0 > limm[0] && xc0 < limm[1] && vc0 > limv[0] && vc0 < limv[1] && sc0 < seriesX2[1];
					ac[1] = xc1 > limm[0] && xc1 < limm[1] && vc1 > limv[0] && vc1 < limv[1] && sc1 < seriesX2[1];
					ac[2] = xnc0 > limm[0] && xnc0 < limm[1] && vnc0 > limv[0] && vnc0 < limv[1] && snc0 < seriesX2[1];
					ac[3] = xnc1 > limm[0] && xnc1 < limm[1] && vnc1 > limv[0] && vnc1 < limv[1] && snc1 < seriesX2[1];
					if(ac[0]) {
						Console.WriteLine("Se acepta Ho de la serie Cuadrados Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Cuadrados Medios");
					}
					if(ac[1]) {
						Console.WriteLine("Se acepta Ho de la serie Productos Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Productos Medios");
					}
					if(ac[2]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Lineal");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Lineal");
					}
					if(ac[3]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Multiplicativo");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Multiplicativo");
					}
					break;
				case "c":
					limm = limitesZ[2];
					limv = limitesX2[2];
					ac = new bool[4];
					ac[0] = xc0 > limm[0] && xc0 < limm[1] && vc0 > limv[0] && vc0 < limv[1] && sc0 < seriesX2[2];
					ac[1] = xc1 > limm[0] && xc1 < limm[1] && vc1 > limv[0] && vc1 < limv[1] && sc1 < seriesX2[2];
					ac[2] = xnc0 > limm[0] && xnc0 < limm[1] && vnc0 > limv[0] && vnc0 < limv[1] && snc0 < seriesX2[2];
					ac[3] = xnc1 > limm[0] && xnc1 < limm[1] && vnc1 > limv[0] && vnc1 < limv[1] && snc1 < seriesX2[2];
					if(ac[0]) {
						Console.WriteLine("Se acepta Ho de la serie Cuadrados Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Cuadrados Medios");
					}
					if(ac[1]) {
						Console.WriteLine("Se acepta Ho de la serie Productos Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Productos Medios");
					}
					if(ac[2]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Lineal");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Lineal");
					}
					if(ac[3]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Multiplicativo");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Multiplicativo");
					}
					break;
			}
		}

		private static void pruebaPoker() {
			double[] poker = {
				analizarPoker(seedsNC[0]),
				analizarPoker(seedsNC[1]),
				analizarPoker(seedsC[0]),
				analizarPoker(seedsC[1]),
			};
			//Console.WriteLine(poker[0] + " " + poker[1] + " " + poker[2] + " " + poker[3]);

			switch(Console.ReadLine()) {
				case "a":
					if(poker[0] < seriesX2[0]) {
						Console.WriteLine("Se acepta Ho de la serie Cuadrados Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Cuadrados Medios");
					}
					if(poker[1] < seriesX2[0]) {
						Console.WriteLine("Se acepta Ho de la serie Productos Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Productos Medios");
					}
					if(poker[2] < seriesX2[0]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Lineal");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Lineal");
					}
					if(poker[3] < seriesX2[0]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Multiplicativo");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Multiplicativo");
					}
					break;
				case "b":
					if(poker[0] < seriesX2[1]) {
						Console.WriteLine("Se acepta Ho de la serie Cuadrados Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Cuadrados Medios");
					}
					if(poker[1] < seriesX2[1]) {
						Console.WriteLine("Se acepta Ho de la serie Productos Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Productos Medios");
					}
					if(poker[2] < seriesX2[1]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Lineal");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Lineal");
					}
					if(poker[3] < seriesX2[1]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Multiplicativo");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Multiplicativo");
					}
					break;
				case "c":
					if(poker[0] < seriesX2[2]) {
						Console.WriteLine("Se acepta Ho de la serie Cuadrados Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Cuadrados Medios");
					}
					if(poker[1] < seriesX2[2]) {
						Console.WriteLine("Se acepta Ho de la serie Productos Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Productos Medios");
					}
					if(poker[2] < seriesX2[2]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Lineal");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Lineal");
					}
					if(poker[3] < seriesX2[2]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Multiplicativo");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Multiplicativo");
					}
					break;
			}
		}

		private static int partition(double[] arr, int l, int h) {
			double pv = arr[h];
			int lowIndex = l - 1;

			for(int j = l; j < h; j++) {
				if(arr[j] <= pv) {
					lowIndex++;
					double temp = arr[lowIndex];
					arr[lowIndex] = arr[j];
					arr[j] = temp;
				}
			}

			double temp1 = arr[lowIndex + 1];
			arr[lowIndex + 1] = arr[h];
			arr[h] = temp1;

			return lowIndex + 1;
		}

		private static void qs(double[] arr, int low, int high) {
			if(low < high) {
				int partitionIndex = partition(arr, low, high);
				qs(arr, low, partitionIndex - 1);
				qs(arr, partitionIndex + 1, high);
			}
		}

		private static void pruebaKS() {
			double[][] sortedC = new double[2][];
			double[][] sortedNC = new double[2][];
			sortedC[0] = (double[]) seedsC[0].Clone();
			sortedC[1] = (double[]) seedsC[1].Clone();
			sortedNC[0] = (double[]) seedsNC[0].Clone();
			sortedNC[1] = (double[]) seedsNC[1].Clone();
			qs(sortedC[0], 0, 99);
			qs(sortedC[1], 0, 99);
			qs(sortedNC[0], 0, 99);
			qs(sortedNC[1], 0, 99);

			double xc0 = media(sortedC[0]);
			double xc1 = media(sortedC[1]);
			double xnc0 = media(sortedNC[0]);
			double xnc1 = media(sortedNC[1]);

			double dc0 = Math.Sqrt(varianza(sortedC[0]));
			double dc1 = Math.Sqrt(varianza(sortedC[1]));
			double dnc0 = Math.Sqrt(varianza(sortedNC[0]));
			double dnc1 = Math.Sqrt(varianza(sortedNC[1]));

			double maxc0 = sortedC[0][sortedC[0].Length - 1];
			double maxc1 = sortedC[1][sortedC[1].Length - 1];
			double maxnc0 = sortedNC[0][sortedNC[0].Length - 1];
			double maxnc1 = sortedNC[1][sortedNC[1].Length - 1];

			double minc0 = sortedC[0][0];
			double minc1 = sortedC[1][0];
			double minnc0 = sortedNC[0][0];
			double minnc1 = sortedNC[1][0];

			double rc0 = maxc0 - minc0;
			double rc1 = maxc1 - minc1;
			double rnc0 = maxnc0 - minnc0;
			double rnc1 = maxnc1 - minnc1;

			//double inter = 7.6;
			//double ir = 10;

			double ac0 = rc0 / 10;
			double ac1 = rc1 / 10;
			double anc0 = rnc0 / 10;
			double anc1 = rnc1 / 10;

			double[] lsc0 = new double[100];
			double[] lsc1 = new double[100];
			double[] lsnc0 = new double[100];
			double[] lsnc1 = new double[100];

			lsc0[0] = minc0 + ac0;
			lsc1[0] = minc1 + ac1;
			lsnc0[0] = minnc0 + anc0;
			lsnc1[0] = minnc1 + anc1;
			for(int i = 1; i < 100; i++) {
				lsc0[i] = lsc0[i - 1] + ac0;
				lsc1[i] = lsc1[i - 1] + ac1;
				lsnc0[i] = lsnc0[i - 1] + anc0;
				lsnc1[i] = lsnc1[i - 1] + anc1;
			}

			int[] foc0 = new int[100];
			int[] foc1 = new int[100];
			int[] fonc0 = new int[100];
			int[] fonc1 = new int[100];

			for(int j = 0; j < 100; j++) {
				double valc0 = sortedC[0][j];
				double valc1 = sortedC[1][j];
				double valnc0 = sortedNC[0][j];
				double valnc1 = sortedNC[1][j];
				if(valc0 < lsc0[0]) {
					foc0[0]++;
				}
				if(valc1 < lsc1[0]) {
					foc1[0]++;
				}
				if(valnc0 < lsnc0[0]) {
					fonc0[0]++;
				}
				if(valnc1 < lsnc1[0]) {
					fonc1[0]++;
				}
			}

			for(int i = 1; i < 100; i++) {
				for(int j = 0; j < 100; j++) {
					double valc0 = sortedC[0][j];
					double valc1 = sortedC[1][j];
					double valnc0 = sortedNC[0][j];
					double valnc1 = sortedNC[1][j];
					if(lsc0[i - 1] < valc0 && valc0 < lsc0[i]) {
						foc0[i]++;
					}
					if(lsc1[i - 1] < valc1 && valc1 < lsc1[i]) {
						foc1[i]++;
					}
					if(lsnc0[i - 1] < valnc0 && valnc0 < lsnc0[i]) {
						fonc0[i]++;
					}
					if(lsnc1[i - 1] < valnc1 && valnc1 < lsnc1[i]) {
						fonc1[i]++;
					}
				}
			}

			//int sumc0 = foc0.Sum();
			//int sumc1 = foc1.Sum();
			//int sumnc0 = fonc0.Sum();
			//int sumnc1 = fonc1.Sum();

			double[] forc0 = new double[100];
			double[] forc1 = new double[100];
			double[] fornc0 = new double[100];
			double[] fornc1 = new double[100];

			for(int i = 0; i < 100; i++) {
				forc0[i] = foc0[i] / 100.0;
				forc1[i] = foc1[i] / 100.0;
				fornc0[i] = fonc0[i] / 100.0;
				fornc1[i] = fonc1[i] / 100.0;
			}

			double[] forac0 = new double[100];
			double[] forac1 = new double[100];
			double[] foranc0 = new double[100];
			double[] foranc1 = new double[100];

			forac0[0] = forc0[0] + forc0[1];
			forac1[0] = forc1[0] + forc1[1];
			foranc0[0] = fornc0[0] + fornc0[1];
			foranc1[0] = fornc1[0] + fornc1[1];
			for(int i = 1; i < 100; i++) {
				forac0[i] = forc0[i] + forac0[i - 1];
				forac1[i] = forc1[i] + forac1[i - 1];
				foranc0[i] = fornc0[i] + foranc0[i - 1];
				foranc1[i] = fornc1[i] + foranc1[i - 1];
			}

			for(int i = 1; i < 100; i++) {
				forac0[i] = forac0[i] > 1 ? 1 : forac0[i];
				forac1[i] = forac1[i] > 1 ? 1 : forac1[i];
				foranc0[i] = foranc0[i] > 1 ? 1 : foranc0[i];
				foranc1[i] = foranc1[i] > 1 ? 1 : foranc1[i];
			}

			double[] zc0 = new double[100];
			double[] zc1 = new double[100];
			double[] znc0 = new double[100];
			double[] znc1 = new double[100];

			double aux;
			for(int i = 0; i < 100; i++) {
				aux = ((int) ((lsc0[i] - xc0) / dc0 * 100)) / 100;
				zc0[i] = aux;
				aux = ((int) ((lsc1[i] - xc1) / dc1 * 100)) / 100;
				zc1[i] = aux;
				aux = ((int) ((lsnc0[i] - xnc0) / dnc0 * 100)) / 100;
				znc0[i] = aux;
				aux = ((int) ((lsnc1[i] - xnc1) / dnc1 * 100)) / 100;
				znc1[i] = aux;
			}

			double[] ferc0 = new double[100];
			double[] ferc1 = new double[100];
			double[] fernc0 = new double[100];
			double[] fernc1 = new double[100];

			for(int i = 0; i < 100; i++) {
				//Console.WriteLine("z:" + zc0[i]);
				//try {
				ferc0[i] = zc0[i] > 3.09 ? 1 : (zc0[i] < -3.09 ? 0 : tableZ[zc0[i]]);
				//} catch(Exception e) {
				//	Console.WriteLine("z:" + zc0[i]);
				//}
				//try {
				ferc1[i] = zc1[i] > 3.09 ? 1 : (zc1[i] < -3.09 ? 0 : tableZ[zc1[i]]);
				//} catch(Exception e) {
				//	Console.WriteLine("z:" + zc1[i]);
				//}
				//try {
				fernc0[i] = znc0[i] > 3.09 ? 1 : (znc0[i] < -3.09 ? 0 : tableZ[znc0[i]]);
				//} catch(Exception e) {
				//	Console.WriteLine("z:" + znc0[i]);
				//}
				//try {
				fernc1[i] = znc1[i] > 3.09 ? 1 : (znc1[i] < -3.09 ? 0 : tableZ[znc1[i]]);
				//} catch(Exception e) {
				//	Console.WriteLine("z:" + znc1[i]);
				//}
			}

			double[] foec0 = new double[100];
			double[] foec1 = new double[100];
			double[] foenc0 = new double[100];
			double[] foenc1 = new double[100];

			for(int i = 0; i < 100; i++) {
				foec0[i] = Math.Abs(forac0[i] - ferc0[i]);
				foec1[i] = Math.Abs(forac1[i] - ferc1[i]);
				foenc0[i] = Math.Abs(foranc0[i] - fernc0[i]);
				foenc1[i] = Math.Abs(foranc1[i] - fernc1[i]);
			}

			//Console.WriteLine(printArray(foec0));
			//Console.WriteLine(printArray(foec1));
			//Console.WriteLine(printArray(foenc0));
			//Console.WriteLine(printArray(foenc1));

			double eksc0 = foec0.Max();
			double eksc1 = foec1.Max();
			double eksnc0 = foenc0.Max();
			double eksnc1 = foenc1.Max();

			//Console.WriteLine(eksc0 + " " + eksc1 + " " + eksnc0 + " " + eksnc1);

			switch(Console.ReadLine()) {
				case "a":
					if(eksc0 < ksValue[0]) {
						Console.WriteLine("Se acepta Ho de la serie Cuadrados Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Cuadrados Medios");
					}
					if(eksc1 < ksValue[0]) {
						Console.WriteLine("Se acepta Ho de la serie Productos Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Productos Medios");
					}
					if(eksnc0 < ksValue[0]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Lineal");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Lineal");
					}
					if(eksnc1 < ksValue[0]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Multiplicativo");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Multiplicativo");
					}
					break;
				case "b":
					if(eksc0 < ksValue[1]) {
						Console.WriteLine("Se acepta Ho de la serie Cuadrados Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Cuadrados Medios");
					}
					if(eksc1 < ksValue[1]) {
						Console.WriteLine("Se acepta Ho de la serie Productos Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Productos Medios");
					}
					if(eksnc0 < ksValue[1]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Lineal");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Lineal");
					}
					if(eksnc1 < ksValue[1]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Multiplicativo");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Multiplicativo");
					}
					break;
				case "c":
					if(eksc0 < ksValue[2]) {
						Console.WriteLine("Se acepta Ho de la serie Cuadrados Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Cuadrados Medios");
					}
					if(eksc1 < ksValue[2]) {
						Console.WriteLine("Se acepta Ho de la serie Productos Medios");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Productos Medios");
					}
					if(eksnc0 < ksValue[2]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Lineal");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Lineal");
					}
					if(eksnc1 < ksValue[2]) {
						Console.WriteLine("Se acepta Ho de la serie Congruencial Multiplicativo");
					} else {
						Console.WriteLine("Se rechaza Ho de la serie Congruencial Multiplicativo");
					}
					break;
			}

		}
		private static void mostrar() {
			switch(Console.ReadLine()) {
				case "a":
					Console.WriteLine(printArray(seedsNC[0]));
					break;
				case "b":
					Console.WriteLine(printArray(seedsNC[1]));
					break;
				case "c":
					Console.WriteLine(printArray(seedsC[0]));
					break;
				case "d":
					Console.WriteLine(printArray(seedsC[1]));
					break;
			}
		}

		private static void exec() {
			Console.WriteLine("Escoja la opción deseada:");
			Console.WriteLine("\ta)\tMostrar Series.");
			Console.WriteLine("\tb)\tPrueba de Poker.");
			Console.WriteLine("\tc)\tPrueba de Kolmogorov-Smirnov.");
			Console.WriteLine("\td)\tPrueba de series, media y varianza.");
			Console.WriteLine("\te)\tGenerar series nuevamente.");
			Console.WriteLine("\totro)\tSalir.");
			Console.Write("Opción: ");
			switch(Console.ReadLine()) {
				case "a":
					Console.WriteLine("Escoja la serie a mostrar:");
					Console.WriteLine("\ta)\tCuadrados Medios.");
					Console.WriteLine("\tb)\tProductos Medios.");
					Console.WriteLine("\tc)\tCongruencial Lineal.");
					Console.WriteLine("\td)\tCongruencial Multiplicativo.");
					Console.WriteLine("\totro)\tVolver.");
					Console.Write("Opción: ");
					mostrar();
					break;
				case "b":
					Console.WriteLine("Escoja la confiabilidad deseada:");
					Console.WriteLine("\ta)\t90%.");
					Console.WriteLine("\tb)\t95%.");
					Console.WriteLine("\tc)\t99%.");
					Console.WriteLine("\totro)\tVolver.");
					Console.Write("Opción: ");
					pruebaPoker();
					break;
				case "c":
					Console.WriteLine("Escoja la confiabilidad deseada:");
					Console.WriteLine("\ta)\t90%.");
					Console.WriteLine("\tb)\t95%.");
					Console.WriteLine("\tc)\t99%.");
					Console.WriteLine("\totro)\tVolver.");
					Console.Write("Opción: ");
					pruebaKS();
					break;
				case "d":
					Console.WriteLine("Escoja la confiabilidad deseada:");
					Console.WriteLine("\ta)\t90%.");
					Console.WriteLine("\tb)\t95%.");
					Console.WriteLine("\tc)\t99%.");
					Console.WriteLine("\totro)\tVolver.");
					Console.Write("Opción: ");
					pruebaSMV();

					break;
				case "e":
					generateSeedC();
					generateSeedNC();
					Console.WriteLine("Series generadas.");
					break;
				default:
					salir = true;
					break;
			}
			Console.WriteLine("\n");
		}

		static void exec2() {
			Console.WriteLine("Escoja la opción deseada:");
			Console.WriteLine("\ta)\tMostrar Series.");
			Console.WriteLine("\tb)\tPrueba de Poker.");
			Console.WriteLine("\tc)\tPrueba de Kolmogorov-Smirnov.");
			Console.WriteLine("\td)\tPrueba de series, media y varianza.");
			Console.WriteLine("\te)\tGenerar series nuevamente.");
			Console.WriteLine("\totro)\tSalir.");
			Console.Write("Opción: ");
			switch(Console.ReadLine()) {
				case "a":
					Console.WriteLine("Escoja la serie a mostrar:");
					Console.WriteLine("\ta)\tCuadrados Medios.");
					Console.WriteLine("\tb)\tProductos Medios.");
					Console.WriteLine("\tc)\tCongruencial Lineal.");
					Console.WriteLine("\td)\tCongruencial Multiplicativo.");
					Console.WriteLine("\totro)\tVolver.");
					Console.Write("Opción: ");
					mostrar();
					break;
				case "b":
					Console.WriteLine("Escoja la confiabilidad deseada:");
					Console.WriteLine("\ta)\t90%.");
					Console.WriteLine("\tb)\t95%.");
					Console.WriteLine("\tc)\t99%.");
					Console.WriteLine("\totro)\tVolver.");
					Console.Write("Opción: ");
					pruebaPoker();
					break;
				case "c":
					Console.WriteLine("Escoja la confiabilidad deseada:");
					Console.WriteLine("\ta)\t90%.");
					Console.WriteLine("\tb)\t95%.");
					Console.WriteLine("\tc)\t99%.");
					Console.WriteLine("\totro)\tVolver.");
					Console.Write("Opción: ");
					pruebaKS();
					break;
				case "d":
					Console.WriteLine("Escoja la confiabilidad deseada:");
					Console.WriteLine("\ta)\t90%.");
					Console.WriteLine("\tb)\t95%.");
					Console.WriteLine("\tc)\t99%.");
					Console.WriteLine("\totro)\tVolver.");
					Console.Write("Opción: ");
					pruebaSMV();
					break;
				case "e":
					generateSeedC();
					generateSeedNC();
					Console.WriteLine("Series generadas.");
					break;
				default:
					salir = true;
					break;
			}
		}
		static bool salir = false;
		static void Main(string[] args) {
			//foreach(int i in numeros) {
			//	Console.WriteLine(i);
			//}
			//Console.WriteLine(numeros.Length);
			fillZ();
			//foreach(var pair in tableZ) {
			//	Console.WriteLine(pair.Key + ": " + pair.Value);
			//}
			generateSeedNC();
			generateSeedC();
			Console.WriteLine("***************************");
			Console.WriteLine("*       Bienvenido        *");
			Console.WriteLine("* Simulación - Práctica 1 *");
			Console.WriteLine("* Daniel Mendoza          *");
			Console.WriteLine("***************************\n");
			exec();
			while(!salir) {
				exec();
			}
			Console.WriteLine("Adios!");
			System.Threading.Thread.Sleep(700);
		}
	}
}
