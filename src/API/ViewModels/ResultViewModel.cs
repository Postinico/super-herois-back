namespace API.ViewModels
{
    public class ResultViewModel
    {
        public ResultViewModel
        (
            bool resultado,
            string menssagem, 
            object data
        )
        {
            Resultado = resultado;
            Menssagem = menssagem;
            Data = data;
        }

        public bool Resultado { get; private set; }

        public string Menssagem { get; private set; }

        public object Data { get; private set; }
    }
}
