using SkiaSharp;

namespace Projeto {
	class Program {
		static void Main(string[] args) {

			int img;

			Console.WriteLine("Qual a quantidade de imagens que você deseja analisar?");
            img = Convert.ToInt32(Console.ReadLine());
			Dictionary<string, int> objetos = new Dictionary<string, int>(img);
			for (int i = 0 ; i < img; i++){
				using (SKBitmap bitmapEntrada = SKBitmap.Decode("C:\\Users\\RODRIGO.CORTESE\\Downloads\\Atv3_Rodrigo_Fernando\\Atv3_Rodrigo_Fernando\\Atv3\\Exercicio 1\\Exercicio1_"+i+".png"),
				bitmapSaidaArit = new SKBitmap(new SKImageInfo(bitmapEntrada.Width, bitmapEntrada.Height, SKColorType.Gray8))) {

					unsafe {
						byte* entrada = (byte*)bitmapEntrada.GetPixels();
						byte* saida = (byte*)bitmapSaidaArit.GetPixels();
						int largura = bitmapEntrada.Width;
						int altura = bitmapEntrada.Height;
						bool considerar8vizinhos = true;
						List<Forma> formas = new List<Forma>();

						int pixelsTotais = bitmapEntrada.Width * bitmapEntrada.Height;

						for (int e = 0, s = 0; s < pixelsTotais; e += 4, s++) {
							if((entrada[e+1] > entrada[e] ) && (entrada[e+1] > entrada[e+2])){
								saida[s] = 0;
							}else{
								saida[s] = 255;
							}
						}

						formas = Forma.DetectarFormas(saida, largura, altura, considerar8vizinhos);
						objetos.Add("Exercicio1_"+i+".png", formas.Count);
					}
					using (FileStream stream = new FileStream("C:\\Users\\RODRIGO.CORTESE\\Downloads\\Atv3_Rodrigo_Fernando\\Atv3_Rodrigo_Fernando\\Atv3\\Exercicio 1\\Exercicio1_"+i+"_saida"+".png", FileMode.OpenOrCreate, FileAccess.Write)) {
						bitmapSaidaArit.Encode(stream, SKEncodedImageFormat.Png, 100);
					}

				}
			}

			var objordenados = objetos.OrderByDescending(kv => kv.Value).ToDictionary(kv => kv.Key, kv => kv.Value);
			for (int i = 0; i < objordenados.Count; i++){
    			Console.WriteLine(objordenados.ElementAt(i).Key + " - "+objordenados.ElementAt(i).Value);
			}
		}
	}
}
