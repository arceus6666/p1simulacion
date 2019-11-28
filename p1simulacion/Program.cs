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

		static double U = 0.5;
		static double sqrt = Math.Sqrt(1200);
		static double[][] limitesX2 = { new double[] { 0.0649, 0.1036 }, new double[] { 0.0619, 0.108 }, new double[] { 0.0561, 0.1168 } };
		static double[][] limitesZ = { new double[] { U - (1.64 / sqrt), U + (1.64 / sqrt) }, new double[] { U - (1.96 / sqrt), U + (1.96 / sqrt) }, new double[] { U - (2.58 / sqrt), U + (2.58 / sqrt) } };

		static Random RND = new Random();


		static double[] seriesX2 = { 24.9958, 27.4884, 32.8013 };

		private static int RandomNumber() {
			int rnd = RND.Next(numeros.Length);
			//Console.Write("rnd: " + rnd + "; ");
			return numeros[rnd];
		}

		private static double media(double[,] n, int s) {
			double x = 0;
			int l = n.GetLength(0);
			for(int i = 0; i < l; i++) {
				x += n[i, s];
			}
			return x / l;
		}

		private static double varianza(double[,] n, int s) {
			double v = 0;
			double x = media(n, s);
			int l = n.GetLength(0);
			for(int i = 0; i < l; i++) {
				double z = n[i, s] - x;
				v += z * z;
			}
			return v / l;
		}

		private static double[] operado(double[,] n, int s) {
			int l = n.GetLength(0);
			double fe = (l - 1) / 16;
			int[] o = new int[16];
			for(int i = 0; i < l - 1; i++) {
				var x = n[i, s];
				var y = n[i + 1, s];
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

		private static string printArray(double[,] arr, int s) {
			string res = "[\n";
			int l = arr.GetLength(0);
			for(int i = 0; i < l - 1; i++) {
				res += "\t" + arr[i, s] + ",\n";
			}
			return res + "\t" + arr[l - 1, s] + "\n]\n";
		}

		//private static double[] limites(double z) {
		//	return new double[] { U - (z / Math.Sqrt(1200)), U + (z / Math.Sqrt(1200)) };
		//}

		static void Main(string[] args) {

			// cuadrados, productos
			double[,] seedsNC = new double[100, 2];
			// lineal, multiplicativo
			double[,] seedsC = new double[100, 2];

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
			Console.WriteLine(xil + " " + al + " " + x0l + " " + c + " " + ml + " " + g + " " + k);
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

				n = Double.Parse(ns);
				nn = Double.Parse(nns);
				seedsNC[i, 0] = n / 10000;
				seedsNC[i, 1] = nn / 10000;
				/****************************/

				/*********Congruentes********/
				seedsC[i, 0] = Math.Round(xil / (ml - 1), 4);
				//Console.WriteLine(xil + " " + (xil / (ml - 1)) + " " + seedsC[i, 0]);
				xil = (al * xil + c) % ml;

				seedsC[i, 1] = Math.Round(xim / (mm - 1), 4);
				xim = (am * xim) % mm;
				/****************************/
				//Console.WriteLine(i + " " + n);

			}

			Console.WriteLine("*******Bienvenido********");
			Console.WriteLine("Escoja la opción deseada:");
			Console.WriteLine("\ta) Mostrar Series.");
			Console.WriteLine("\tb) Prueba de Poker.");
			Console.WriteLine("\tc) Prueba de Kolmogorov-Smirnov.");
			Console.WriteLine("\td) Prueba de series, media y varianza.");
			Console.WriteLine("\te) Salir.");
			Console.Write("Opción: ");
			bool salir = false;
			// Use a switch statement to do the math.
			switch(Console.ReadLine()) {
				case "a":
					Console.WriteLine("Escoja la serie a mostrar:");
					Console.WriteLine("\ta) Cuadrados Medios.");
					Console.WriteLine("\tb) Productos Medios.");
					Console.WriteLine("\tc) Congruencial Lineal.");
					Console.WriteLine("\td) Congruencial Multiplicativo.");
					Console.WriteLine("\totro) Volver.");
					Console.Write("Opción: ");
					switch(Console.ReadLine()) {
						case "a":
							Console.WriteLine(printArray(seedsNC, 0));
							break;
						case "b":
							Console.WriteLine(printArray(seedsNC, 1));
							break;
						case "c":
							Console.WriteLine(printArray(seedsC, 0));
							break;
						case "d":
							Console.WriteLine(printArray(seedsC, 1));
							break;
					}
					break;
				case "b":
					Console.WriteLine("TBA");
					break;
				case "c":
					Console.WriteLine("TBA");
					break;
				case "d":
					Console.WriteLine("Escoja la confiabilidad deseada:");
					Console.WriteLine("\ta) 90%.");
					Console.WriteLine("\tb) 95%.");
					Console.WriteLine("\tc) 99%.");
					Console.WriteLine("\totro) Volver.");
					Console.Write("Opción: ");
					double xc0 = media(seedsC, 0);
					double xc1 = media(seedsC, 1);
					double xnc0 = media(seedsNC, 0);
					double xnc1 = media(seedsNC, 1);
					double vc0 = varianza(seedsC, 0);
					double vc1 = varianza(seedsC, 1);
					double vnc0 = varianza(seedsNC, 0);
					double vnc1 = varianza(seedsNC, 1);
					double sc0 = operado(seedsC, 0).Sum();
					double sc1 = operado(seedsC, 1).Sum();
					double snc0 = operado(seedsNC, 0).Sum();
					double snc1 = operado(seedsNC, 1).Sum();

					switch(Console.ReadLine()) {
						case "a":
							double[] limm = limitesZ[0];
							double[] limv = limitesX2[0];
							bool[] ac = new bool[4];
							ac[0] = xc0 > limm[0] && xc0 < limm[1] && vc0 > limv[0] && vc0 < limv[1] && sc0 < seriesX2[0];
							ac[1] = xc1 > limm[0] && xc1 < limm[1] && vc1 > limv[0] && vc1 < limv[1] && sc1 < seriesX2[0];
							ac[2] = xnc0 > limm[0] && xnc0 < limm[1] && vnc0 > limv[0] && vnc0 < limv[1] && snc0 < seriesX2[0];
							ac[3] = xnc1 > limm[0] && xnc1 < limm[1] && vnc1 > limv[0] && vnc1 < limv[1] && snc1 < seriesX2[0];
							if(ac[0]) {
								Console.WriteLine("Se acepta Ho de la serie Cuadrados Medios");
								//Console.WriteLine("Se acepta Ho de la serie Cuadrados Medios\n [" +
								//  xc0 + " " + (xc0 > limm[0] && xc0 < limm[1]) + "] [" + vc0 + " " + (vc0 > limv[0] && vc0 < limv[1]) + "] [" + sc0 + " " + (sc0 < seriesX2[0]) + "]");
							} else {
								Console.WriteLine("Se rechaza Ho de la serie Cuadrados Medios");
								//Console.WriteLine("Se rechaza Ho de la serie Cuadrados Medios\n [" +
								//	xc0 + " " + (xc0 > limm[0] && xc0 < limm[1]) + "] [" + vc0 + " " + (vc0 > limv[0] && vc0 < limv[1]) + "] [" + sc0 + " " + (sc0 < seriesX2[0]) + "]");
								//Console.WriteLine(limm[0] + " " + limm[1]);
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

					break;
				case "e":
					salir = true;
					break;
			}
			// Wait for the user to respond before closing.
			while(!salir) {
				Console.WriteLine("Escoja la opción deseada:");
				Console.WriteLine("\ta) Mostrar Series.");
				Console.WriteLine("\tb) Prueba de Poker.");
				Console.WriteLine("\tc) Prueba de Kolmogorov-Smirnov.");
				Console.WriteLine("\td) Prueba de series, media y varianza.");
				Console.WriteLine("\te) Salir.");
				Console.Write("Opción: ");
				// Use a switch statement to do the math.
				switch(Console.ReadLine()) {
					case "a":
						Console.WriteLine("Escoja la serie a mostrar:");
						Console.WriteLine("\ta) Cuadrados Medios.");
						Console.WriteLine("\tb) Productos Medios.");
						Console.WriteLine("\tc) Congruencial Lineal.");
						Console.WriteLine("\td) Congruencial Multiplicativo.");
						Console.WriteLine("\totro) Volver.");
						Console.Write("Opción: ");
						switch(Console.ReadLine()) {
							case "a":
								Console.WriteLine(printArray(seedsNC, 0));
								break;
							case "b":
								Console.WriteLine(printArray(seedsNC, 1));
								break;
							case "c":
								Console.WriteLine(printArray(seedsC, 0));
								break;
							case "d":
								Console.WriteLine(printArray(seedsC, 1));
								break;
						}
						break;
					case "b":
						Console.WriteLine("TBA");
						break;
					case "c":
						Console.WriteLine("TBA");
						break;
					case "d":
						Console.WriteLine("Escoja la confiabilidad deseada:");
						Console.WriteLine("\ta) 90%.");
						Console.WriteLine("\tb) 95%.");
						Console.WriteLine("\tc) 99%.");
						Console.WriteLine("\totro) Volver.");
						Console.Write("Opción: ");
						double xc0 = media(seedsC, 0);
						double xc1 = media(seedsC, 1);
						double xnc0 = media(seedsNC, 0);
						double xnc1 = media(seedsNC, 1);
						double vc0 = varianza(seedsC, 0);
						double vc1 = varianza(seedsC, 1);
						double vnc0 = varianza(seedsNC, 0);
						double vnc1 = varianza(seedsNC, 1);
						double sc0 = operado(seedsC, 0).Sum();
						double sc1 = operado(seedsC, 1).Sum();
						double snc0 = operado(seedsNC, 0).Sum();
						double snc1 = operado(seedsNC, 1).Sum();

						switch(Console.ReadLine()) {
							case "a":
								double[] limm = limitesZ[0];
								double[] limv = limitesX2[0];
								bool[] ac = new bool[4];
								ac[0] = xc0 > limm[0] && xc0 < limm[1] && vc0 > limv[0] && vc0 < limv[1] && sc0 < seriesX2[0];
								ac[1] = xc1 > limm[0] && xc1 < limm[1] && vc1 > limv[0] && vc1 < limv[1] && sc1 < seriesX2[0];
								ac[2] = xnc0 > limm[0] && xnc0 < limm[1] && vnc0 > limv[0] && vnc0 < limv[1] && snc0 < seriesX2[0];
								ac[3] = xnc1 > limm[0] && xnc1 < limm[1] && vnc1 > limv[0] && vnc1 < limv[1] && snc1 < seriesX2[0];
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

						break;
					case "e":
						salir = true;
						break;
				}
			}
			Console.WriteLine("Adios!");
			System.Threading.Thread.Sleep(500);
		}
	}
}
