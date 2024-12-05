## EXECUTANDO A APLICAÇÃO E O TESTE DE INTEGRAÇÃO:

--------------------------------------------------------------------------------------------
ATENÇÃO: A APLICAÇÃO DEVE SER EXECUTADA NA VERSÃO 17.12.3 DO MICROSOFT VISUAL STUDIO.
CASO A VERSÃO INSTALADA DO VISUAL STUDIO NÃO ESTEJA NA VERSÃO MAIS RECENTE ATÉ 04/12/2024,
ATUALIZE-A ANTES DE EXECUTÁ-LA LOCALMENTE EM SEU COMPUTADOR.
--------------------------------------------------------------------------------------------

Basta executar a aplicação no Visual Studio e o Swagger será inicializado.

Se preferir, pode acessá-lo através do endereço http://localhost:5084/swagger/index.html pelo seu navegador de preferência.

Faça o upload do arquivo CSV através do Swagger, utilizando o botão "Selecionar arquivo" do método /api/movies/upload.

Após receber a mensagem "File processed successfully." no ResponseBody, pode executar o método /api/movies/award-intervals
para obter os dados carregados em memória, devidamente processados de acordo com as regras de negócio definidas na especificação técnica.

O teste de integração deve ser executado através do gerenciador de teste, no menu Teste > Gerenciador de Teste do Visual Studio.
