# Supervisão e controle de nível com lógica Fuzzy: Uma integração baseada em OPC

## Resumo

No âmbito da automação industrial, a integração entre sistemas de controle constitui uma necessidade fundamental para o monitoramento e controle eficiente de processos produtivos. Este trabalho apresenta o desenvolvimento de um sistema de supervisão e controle de nível baseado em lógica Fuzzy, integrado através do protocolo OPC (Open Platform Communications).

A lógica Fuzzy, técnica de inteligência artificial que possibilita o tratamento de incertezas e imprecisões inerentes aos processos industriais, demonstra particular adequação para o controle de plantas de nível. Tais sistemas, amplamente utilizados nas indústrias petroquímica, farmacêutica e de alimentos e bebidas, caracterizam-se por sua natureza não linear e frequentemente multivariável, apresentando desafios significativos para técnicas de controle convencionais.

O objetivo principal deste trabalho consiste na implementação de uma arquitetura de integração entre uma API responsável pelo controle Fuzzy e uma planta de nível simulada, utilizando o protocolo OPC como meio de comunicação. Esta abordagem visa demonstrar a viabilidade da aplicação de técnicas de inteligência artificial em ambientes industriais reais, aproveitando-se da padronização e ampla adoção do protocolo OPC na indústria moderna.

## 1. Introdução

A quarta revolução industrial, denominada Indústria 4.0, representa um paradigma transformador na manufatura global, caracterizada pela integração sistemática de tecnologias digitais avançadas nos processos produtivos. Este movimento revolucionário fundamenta-se na conectividade entre sistemas físicos e digitais, proporcionando maior eficiência, flexibilidade e sustentabilidade aos processos industriais (SCHWAB, 2016). Neste contexto, a automação industrial emerge como elemento central, demandando investimentos significativos em tecnologias que possibilitem a otimização de processos e a redução de custos operacionais.

Os investimentos em tecnologia para automatização têm experimentado crescimento exponencial nas últimas décadas, impulsionados pela necessidade de competitividade global e pela busca por processos mais eficientes e sustentáveis. Segundo dados da Associação Brasileira de Automação Industrial (ABAI), o mercado brasileiro de automação industrial registrou crescimento consistente, evidenciando a importância estratégica deste setor para o desenvolvimento econômico nacional. Tais investimentos abrangem desde a modernização de equipamentos até a implementação de sistemas de controle avançados, visando à otimização de recursos e ao aumento da produtividade.

A utilização de inteligência artificial na indústria constitui uma das principais tendências da Indústria 4.0, oferecendo soluções inovadoras para desafios complexos de controle e otimização. Técnicas como redes neurais, algoritmos genéticos e lógica Fuzzy têm demonstrado eficácia significativa na resolução de problemas industriais que envolvem incertezas, não linearidades e múltiplas variáveis. A lógica Fuzzy, em particular, destaca-se pela sua capacidade de modelar conhecimento humano especializado e tratar informações imprecisas, características comuns em ambientes industriais reais.

O controle automático representa um dos pilares fundamentais da automação industrial moderna, possibilitando a manutenção de variáveis de processo dentro de faixas operacionais desejadas sem intervenção humana direta. A implementação de sistemas de controle automático resulta em benefícios tangíveis, incluindo maior precisão no controle de processos, redução de variabilidade na qualidade do produto, diminuição de custos operacionais e melhoria nas condições de segurança operacional. Ademais, tais sistemas contribuem para a redução do impacto ambiental através da otimização do uso de recursos naturais e energia.

No contexto específico da automação industrial, o controle de nível assume importância crítica em diversas aplicações, desde o armazenamento de matérias-primas até o processamento final de produtos. Sistemas de controle de nível são essenciais para garantir a continuidade operacional, prevenir perdas de produto por transbordamento, evitar danos a equipamentos por operação em vazio e assegurar a qualidade do produto final. Nas indústrias petroquímica, farmacêutica e de alimentos e bebidas, o controle preciso de nível em tanques e reatores é fundamental para manter as condições ideais de processo e atender aos rigorosos padrões de qualidade e segurança exigidos por essas indústrias.

Diante deste cenário, o presente trabalho propõe uma abordagem para o controle de nível industrial, integrando a lógica Fuzzy com o protocolo OPC, visando demonstrar a viabilidade da aplicação de técnicas de inteligência artificial em ambientes industriais reais através de uma arquitetura de integração padronizada e amplamente adotada pela indústria.

### 1.1 Revisão bibliográfica

#### 1.1.1 Importância do controle de nível

O controle de nível em processos industriais constitui uma das variáveis mais críticas para o funcionamento seguro e eficiente de plantas produtivas. A relevância deste tipo de controle é evidenciada em diversos setores industriais, desde aplicações didáticas até implementações em larga escala industrial.

**BACOVIS (2016)** realizaram um estudo comparativo entre controladores Fuzzy e PID aplicados em uma planta didática de nível de líquido, demonstrando que o controle adequado de nível é fundamental para o aprendizado e compreensão dos princípios de automação industrial. O trabalho evidencia que plantas de nível são amplamente utilizadas em ambientes educacionais devido à sua capacidade de representar fielmente os desafios encontrados em processos industriais reais, incluindo não linearidades, tempo morto e distúrbios externos.

Na indústria de alimentos e bebidas, **Gomes (2022)** investigou a aplicação da lógica Fuzzy no controle de qualidade na produção de cerveja, onde o controle preciso de nível em tanques de fermentação e maturação é crucial para garantir a qualidade do produto final. O autor demonstra que variações não controladas no nível podem afetar diretamente características organolépticas da cerveja, como aroma, sabor e teor alcoólico, evidenciando a importância crítica do controle de nível em processos biotecnológicos.

Em aplicações de saneamento e abastecimento público, **SILVEIRA et al. (2021)** apresentaram um estudo sobre a aplicação da lógica Fuzzy no controle de nível de reservatório de abastecimento de água, destacando que o controle eficiente de nível é essencial para garantir o fornecimento contínuo e adequado de água potável. Os autores enfatizam que falhas em sistemas de controle de nível podem resultar em desabastecimento populacional, desperdício de recursos hídricos e comprometimento da qualidade da água distribuída.

A importância do controle de nível transcende aspectos puramente técnicos, envolvendo questões econômicas, ambientais e de segurança. Sistemas de controle de nível inadequados podem resultar em perdas significativas de produto, consumo excessivo de energia, riscos ambientais por vazamentos e comprometimento da segurança operacional. Ademais, o controle preciso de nível é fundamental para otimizar o uso de recursos naturais e contribuir para a sustentabilidade de processos industriais.

#### 1.1.2 Utilização do OPC em plantas industriais

O protocolo OPC (Open Platform Communications) e sua evolução para OPC UA (Unified Architecture) têm revolucionado a comunicação industrial, oferecendo soluções padronizadas para a integração de sistemas heterogêneos em ambientes produtivos. A aplicação desta tecnologia em plantas industriais demonstra sua versatilidade e eficácia na resolução de desafios complexos de interoperabilidade e digitalização.

**Carvalho et al. (2023)** investigaram a digitalização de uma planta industrial utilizando o protocolo de comunicação OPC-UA, focando no sistema educacional CP Lab da Festo. O estudo demonstra como a Indústria 4.0 está impulsionando transformações profundas na indústria através de soluções que aprimoram a produção e elevam a qualidade dos produtos. Os autores evidenciam que tecnologias como IoT, sistemas ciberfísicos, Big Data e computação em nuvem estão remodelando as operações empresariais através da integração de sistemas, criação de fábricas inteligentes e melhoria da eficiência. A pesquisa destaca que a utilização do protocolo OPC UA para conectar módulos ciberfísicos e dispositivos à nuvem possibilita análise de dados em tempo real para identificar status atual, problemas potenciais e tomar decisões mais eficazes.

Em aplicações de manufatura flexível, **Silva (2023)** desenvolveu um sistema de instrumentação para uma planta de manufatura flexível utilizando o padrão OPC-UA embarcado no microcontrolador ESP32. O trabalho evidencia que o OPC-UA é um padrão de comunicação industrial que permite transferência de dados segura e confiável entre diferentes sistemas e dispositivos em setores industriais. O estudo demonstra a viabilidade de implementar servidores OPC-UA em dispositivos embarcados de baixo custo, estabelecendo comunicação entre servidores OPC-UA utilizando a ferramenta Node-RED para transferir dados do sistema de medição para controladores lógicos programáveis (CLP) presentes na planta industrial.

**Petrocchi (2024)** apresentou uma implementação da integração entre tecnologia RFID e o padrão OPC UA aplicada a um sistema de manufatura flexível no laboratório da UNESP campus Sorocaba. O trabalho demonstra que o padrão OPC UA fornece uma troca segura de informações e dados na área da automação industrial, utilizando um protocolo padrão extensível que pode ser empregado em multiplataformas. A pesquisa evidencia que é possível realizar alterações das informações através do sistema de servidor e cliente graças às funcionalidades de acesso aos dados do OPC UA. O desenvolvimento utilizou o ambiente Node-RED para configuração automática do leitor RFID e criação do servidor OPC UA, permitindo leitura e escrita de dados tanto pelo servidor quanto pela interface gráfica.

**Souza (2024)** conduziu um estudo de caso sobre controle e supervisão de processos utilizando o padrão OPC, focando na superação dos desafios de integração entre equipamentos e softwares de diferentes fabricantes devido a protocolos proprietários. O trabalho utilizou o CLP Codesys e o ambiente simulado do Factory IO, com foco na planta industrial Sorting Station, demonstrando como o padrão OPC UA promove a troca de dados e comandos entre sistemas distintos. A pesquisa explora as possibilidades tecnológicas oferecidas por essa integração, evidenciando a capacidade do OPC UA em conectar e comunicar softwares e dispositivos de diferentes fabricantes.

A utilização do OPC em plantas industriais transcende aspectos puramente técnicos, representando um facilitador fundamental para a implementação da Indústria 4.0. Os trabalhos analisados demonstram que a adoção do protocolo OPC UA resulta em maior flexibilidade operacional, redução de custos de integração, melhoria na interoperabilidade entre sistemas e facilita a migração para arquiteturas mais modernas. Ademais, a capacidade do OPC UA de operar em dispositivos embarcados de baixo custo democratiza o acesso a tecnologias avançadas de comunicação industrial, possibilitando a modernização de plantas existentes com investimentos reduzidos.

#### 1.1.3 Controle inteligente e OPC em aplicações industriais

A convergência entre técnicas de controle inteligente e protocolos de comunicação padronizados como o OPC representa uma das principais tendências na automação industrial moderna. Esta integração possibilita o desenvolvimento de sistemas de controle mais sofisticados, flexíveis e adaptáveis às demandas crescentes da Indústria 4.0, oferecendo soluções inovadoras para desafios complexos de controle e manutenção industrial.

**Oliveira Junior (2023)** desenvolveu um estudo sobre gerenciamento de nível em reservatório de líquidos utilizando lógica Fuzzy e controle PID, implementando uma arquitetura integrada baseada em OPC para comunicação entre sistemas. O trabalho apresenta um sistema amplamente utilizado nas instalações industriais: o controle de nível de tanque, realizado através do software CodeSys e Matlab/Simulink, com simulação no ambiente Factory I/O. A pesquisa desenvolveu dois controladores distintos: um baseado em lógica Fuzzy e outro em controle PID, utilizando o protocolo de comunicação OPC para estabelecer a comunicação entre o CLP, Matlab e Factory I/O. O autor evidencia que a utilização da ferramenta Fuzzy Logic Designer do Matlab, em conjunto com o Simulink, proporcionou análise detalhada do comportamento do sistema, demonstrando controle satisfatório do sistema simulado de tanque. Esta abordagem integrada ilustra como a combinação de técnicas de controle inteligente com protocolos de comunicação padronizados pode resultar em sistemas de controle mais eficazes e facilmente integráveis a arquiteturas industriais existentes.

**Coretti (2025)** investigou a automação com redes inteligentes para manutenção de sistemas de controle de processos industriais, focando no desenvolvimento de mecanismos para digitalização da malha de controle de vazão e tomada de decisão para análise da manutenção de sistemas automatizados. O trabalho utiliza conceitos da Indústria 4.0 e algoritmos de inteligência artificial para melhorar a visibilidade das malhas de controle fechadas e dos ativos da fábrica. A pesquisa propõe uma abordagem que facilita à equipe industrial de manutenção e operação maior facilidade na gestão dos sistemas de automação e tomada de decisões precisas, sem grandes interrupções do processo produtivo. O autor demonstra que a implementação de redes industriais inteligentes, combinada com manutenção proativa e softwares industriais, permite gestão mais eficiente das fábricas com custos reduzidos. A proposta inclui interface de digitalização da malha de controle livre, configurada em blocos funcionais na web, evidenciando a tendência de integração entre sistemas de automação tradicionais e plataformas digitais modernas.

A integração entre controle inteligente e OPC em aplicações industriais representa uma evolução natural dos sistemas de automação, proporcionando benefícios significativos em termos de flexibilidade, manutenibilidade e capacidade de integração. Os trabalhos analisados demonstram que esta abordagem permite não apenas melhor desempenho de controle através de técnicas inteligentes como lógica Fuzzy, mas também facilita a implementação de estratégias de manutenção preditiva e proativa através da padronização de comunicação proporcionada pelo OPC.

Ademais, a utilização de protocolos OPC em sistemas de controle inteligente contribui para a democratização do acesso a tecnologias avançadas, permitindo que empresas de diferentes portes implementem soluções sofisticadas de automação sem dependência de fornecedores específicos. Esta característica é fundamental para a competitividade industrial no contexto da Indústria 4.0, onde a capacidade de adaptação rápida às mudanças tecnológicas e de mercado constitui vantagem estratégica crítica.

### 1.2 Objetivos

Os objetivos deste trabalho podem ser definidos da seguinte forma:

#### 1.2.1 Objetivo geral

O objetivo geral deste trabalho consiste no desenvolvimento e implementação de uma arquitetura integrada de controle de nível industrial baseada em lógica Fuzzy, utilizando o protocolo OPC como meio de comunicação padronizado. Especificamente, pretende-se desenvolver uma API especializada para realizar o controle Fuzzy de sistemas de nível, integrá-la a uma planta de nível simulada através do protocolo OPC, e demonstrar o desempenho desta integração em termos de eficácia de controle, estabilidade do sistema e viabilidade de implementação em ambientes industriais reais.

Esta abordagem visa contribuir para o avanço do conhecimento na área de automação industrial inteligente, demonstrando a aplicabilidade prática da combinação entre técnicas de inteligência artificial e protocolos de comunicação padronizados na solução de problemas complexos de controle de processos industriais.

#### 1.2.2 Objetivos específicos

Para alcançar o objetivo geral proposto, foram definidos os seguintes objetivos específicos:

- **Caracterização do sistema de controle**: Extrair os parâmetros Fuzzy da planta de nível, identificando e definindo as variáveis de entrada e saída do sistema, bem como suas respectivas funções de pertinência e regras de inferência necessárias para o controle eficaz do processo.

- **Desenvolvimento da API de controle**: Desenvolver uma API especializada utilizando a linguagem Python, implementando os algoritmos de lógica Fuzzy baseados nos parâmetros previamente definidos, garantindo flexibilidade, robustez e facilidade de integração com sistemas externos.

- **Implementação da aplicação de integração**: Desenvolver uma aplicação utilizando a linguagem C# para promover a integração entre a API de controle Fuzzy e a planta de nível, implementando os protocolos de comunicação OPC necessários para estabelecer a troca de dados em tempo real entre os sistemas.

- **Desenvolvimento da interface humano-máquina**: Criar uma IHM (Interface Humano-Máquina) intuitiva e funcional que permita a configuração dinâmica da API e do cliente OPC, bem como a visualização em tempo real do controle de nível, proporcionando monitoramento eficiente e facilidade de operação do sistema integrado.

- **Análise de desempenho**: Analisar os resultados obtidos através de testes e simulações, avaliando parâmetros como estabilidade do controle, tempo de resposta, precisão no seguimento de referência, robustez a distúrbios e eficácia da comunicação OPC, documentando as conclusões e contribuições do trabalho para a área de automação industrial.

### 1.3 Estrutura do texto

Este trabalho está organizado em seis capítulos, estruturados de forma a apresentar uma sequência lógica e progressiva do desenvolvimento da pesquisa, desde a fundamentação teórica até a análise dos resultados obtidos.

**Capítulo 1 - Introdução**: Apresenta o contexto geral da pesquisa, abordando a importância do controle de nível em plantas industriais, a utilização do protocolo OPC em ambientes produtivos e a convergência entre controle inteligente e comunicação OPC. Este capítulo também define os objetivos do trabalho e estabelece a justificativa para a integração de técnicas de lógica Fuzzy com protocolos de comunicação padronizados.

**Capítulo 2 - Fundamentação Teórica**: Explora a evolução histórica dos sistemas de controle, desde as técnicas clássicas até as abordagens modernas, destacando onde o controle Fuzzy se posiciona nesta evolução tecnológica. Adicionalmente, examina a utilização de protocolos de comunicação em plantas industriais, enfatizando a importância da padronização e interoperabilidade para a Indústria 4.0.

**Capítulo 3 - Metodologia**: Detalha a metodologia empregada no desenvolvimento do trabalho, incluindo a justificativa para a escolha da planta de nível como objeto de estudo, a seleção da tecnologia Python para criação da API de controle Fuzzy, e a definição da tecnologia para desenvolvimento da Interface Humano-Máquina (IHM), fundamentando cada decisão tecnológica com base em critérios técnicos e práticos.

**Capítulo 4 - Desenvolvimento**: Apresenta o processo de desenvolvimento dos componentes principais do sistema integrado, incluindo a implementação da API de controle Fuzzy, o desenvolvimento da IHM para monitoramento e configuração, e a criação do cliente OPC UA para estabelecer a comunicação entre os diferentes módulos do sistema.

**Capítulo 5 - Resultados e Discussão**: Analisa os resultados obtidos através da integração entre a API de controle Fuzzy e a planta de nível via protocolo OPC, apresentando métricas de desempenho, estabilidade do sistema e eficácia da comunicação, bem como a discussão crítica dos resultados em relação aos objetivos propostos.

**Capítulo 6 - Conclusões e Trabalhos Futuros**: Sintetiza as principais conclusões do trabalho, destacando as contribuições para a área de automação industrial e controle inteligente, além de propor direcionamentos para trabalhos futuros que possam ampliar e aprofundar os resultados obtidos nesta pesquisa.

## 2. Fundamentação Teórica

A evolução dos sistemas de controle automático reflete o desenvolvimento tecnológico da humanidade, desde os primeiros mecanismos de realimentação até os modernos sistemas baseados em inteligência artificial. Os primórdios do controle automático remontam aos reguladores mecânicos do século XVIII, como o regulador centrífugo de James Watt para máquinas a vapor, estabelecendo os fundamentos do controle por realimentação. A formalização matemática destes conceitos ocorreu no século XX, com o desenvolvimento da teoria de controle clássico, caracterizada pela utilização de técnicas baseadas em função de transferência e análise no domínio da frequência (OGATA, 2010).

O controlador PID (Proporcional-Integral-Derivativo) emergiu como a solução mais amplamente adotada no controle clássico, oferecendo simplicidade de implementação e eficácia em uma ampla gama de aplicações industriais. Sua popularidade deve-se à capacidade de fornecer controle estável e responsivo através do ajuste de três parâmetros fundamentais: ganho proporcional, tempo integral e tempo derivativo (VILLAÇA; SILVEIRA, 2013).

A década de 1960 marcou o início da era do controle moderno, caracterizada pela representação de sistemas em espaço de estados e pela utilização de métodos de otimização. Esta abordagem permitiu o tratamento de sistemas multivariáveis e não lineares de forma mais sistemática, superando limitações do controle clássico. O controle moderno introduziu conceitos como controlabilidade, observabilidade e estabilidade no sentido de Lyapunov, com técnicas como o regulador linear quadrático (LQR) e o filtro de Kalman proporcionando ferramentas poderosas para o projeto de controladores ótimos (FRANKLIN et al., 2015).

O reconhecimento de que os modelos matemáticos são sempre aproximações da realidade levou ao desenvolvimento do controle robusto nas décadas de 1980 e 1990. Esta abordagem considera explicitamente as incertezas do modelo, garantindo estabilidade e desempenho satisfatório mesmo na presença de variações paramétricas e distúrbios não modelados. Técnicas como H∞ e μ-síntese forneceram métodos sistemáticos para o projeto de controladores robustos (ZHOU et al., 1996).

A lógica Fuzzy, introduzida por Lotfi Zadeh em 1965, representa uma abordagem fundamentalmente diferente para o controle de sistemas, baseada na capacidade humana de raciocinar com informações imprecisas e tomar decisões em ambientes de incerteza. Esta técnica emerge como uma ponte entre o controle convencional e a inteligência artificial aplicada, posicionando-se como uma alternativa às limitações dos métodos clássicos e modernos (ZADEH, 1965).

O desenvolvimento do controle Fuzzy surge em resposta às dificuldades encontradas pelos métodos convencionais em lidar com sistemas complexos, não lineares e mal definidos matematicamente. Enquanto o controle clássico e moderno dependem de modelos matemáticos precisos, o controle Fuzzy permite a incorporação de conhecimento heurístico e experiência operacional diretamente no algoritmo de controle, dispensando a necessidade de modelagem matemática rigorosa (MAMDANI; ASSILIAN, 1975).

O controle Fuzzy posiciona-se na evolução dos sistemas de controle como uma abordagem complementar que preenche lacunas deixadas pelos métodos convencionais. Sua principal contribuição reside na capacidade de tratar incertezas e imprecisões de forma natural, oferecendo robustez e simplicidade de implementação em aplicações onde modelos matemáticos precisos são difíceis de obter ou onde existe conhecimento heurístico valioso disponível (ROSS, 2010).

A integração eficiente de sistemas em ambientes industriais depende fundamentalmente da capacidade de comunicação entre dispositivos heterogêneos. A evolução dos protocolos de comunicação industrial reflete a crescente necessidade de interoperabilidade, segurança e eficiência na troca de informações. A comunicação industrial evoluiu de sistemas proprietários isolados para padrões abertos e interoperáveis, onde inicialmente cada fabricante desenvolvia seus próprios protocolos, resultando em "ilhas de automação" com limitada capacidade de integração (STENERSON; FIXED, 2015).

A necessidade de interoperabilidade levou ao desenvolvimento de padrões como Modbus, Profibus e DeviceNet. O OPC (Open Platform Communications) foi desenvolvido para resolver problemas de interoperabilidade em automação industrial, estabelecendo um padrão para comunicação entre aplicações de diferentes fornecedores. Baseado inicialmente na tecnologia DCOM da Microsoft, o OPC Classic proporcionou avanços significativos, mas suas limitações, incluindo dependência de plataforma e problemas de segurança, levaram ao desenvolvimento do OPC UA (Unified Architecture) (MAHNKE et al., 2009).

**Carvalho et al. (2023)** investigaram a digitalização de uma planta industrial utilizando o protocolo de comunicação OPC-UA, demonstrando como a Indústria 4.0 está impulsionando transformações profundas na indústria através de soluções que aprimoram a produção. A pesquisa destaca que a utilização do protocolo OPC UA para conectar módulos ciberfísicos e dispositivos à nuvem possibilita análise de dados em tempo real para identificar status atual, problemas potenciais e tomar decisões mais eficazes.

**Souza (2024)** conduziu um estudo de caso sobre controle e supervisão de processos utilizando o padrão OPC, focando na superação dos desafios de integração entre equipamentos e softwares de diferentes fabricantes devido a protocolos proprietários. O trabalho utilizou o CLP Codesys e o ambiente simulado do Factory IO, demonstrando como o padrão OPC UA promove a troca de dados e comandos entre sistemas distintos.

O OPC UA representa uma arquitetura completamente nova, projetada para ser independente de plataforma, segura, escalável e orientada a serviços. Utiliza uma arquitetura em camadas que separa a lógica de aplicação dos detalhes de comunicação, com modelo de informação baseado em uma estrutura hierárquica de nós e referências que permite representar informações complexas de forma padronizada e extensível (LEITNER; MAHNKE, 2006).

A utilização de protocolos de comunicação padronizados em plantas industriais transcende aspectos puramente técnicos, representando um facilitador fundamental para a implementação da Indústria 4.0. O OPC UA alinha-se perfeitamente com os princípios da Indústria 4.0, facilitando a integração de sistemas ciberfísicos, permitindo comunicação entre sistemas heterogêneos, suportando diversos paradigmas de comunicação e adaptando-se desde dispositivos de campo até sistemas na nuvem (JASPERNEITE, 2012).

A combinação de protocolos padronizados como OPC UA com técnicas de controle inteligente, incluindo lógica Fuzzy, oferece oportunidades únicas para a criação de sistemas de automação mais eficientes e adaptáveis. Esta integração permite distribuição de inteligência em diferentes níveis da hierarquia de controle, flexibilidade arquitetural onde mudanças nos algoritmos de controle não afetam a infraestrutura de comunicação, reutilização de componentes e manutenibilidade através da separação clara entre lógica de controle e comunicação.

**Oliveira Junior (2023)** desenvolveu um estudo sobre gerenciamento de nível em reservatório de líquidos utilizando lógica Fuzzy e controle PID, implementando uma arquitetura integrada baseada em OPC para comunicação entre sistemas. O trabalho demonstrou controle satisfatório do sistema simulado de tanque, ilustrando como a combinação de técnicas de controle inteligente com protocolos de comunicação padronizados pode resultar em sistemas de controle mais eficazes e facilmente integráveis.

A convergência entre controle inteligente e comunicação padronizada representa um paradigma fundamental para a automação industrial moderna, proporcionando a base tecnológica necessária para implementar os conceitos da Indústria 4.0 de forma eficaz e sustentável. Esta integração facilita a implementação de estratégias de manutenção preditiva e proativa, contribui para a democratização do acesso a tecnologias avançadas e permite que empresas de diferentes portes implementem soluções sofisticadas de automação sem dependência de fornecedores específicos.

## Referências

1. Schwab, K. (2016). "A Quarta Revolução Industrial". *Revista Parcerias Estratégicas*, 21(43), 13-26. <https://www.redalyc.org/pdf/4966/496654013004.pdf>

2. Bacovis, N.A. et al. (2016). "Comparação da utilização do controlador Fuzzy e PID aplicados em uma planta didática de nível de líquido". Trabalho de Conclusão de Curso - Universidade Tecnológica Federal do Paraná. <https://riut.utfpr.edu.br/jspui/handle/1/16187>

3. Gomes, K.E. (2022). "Aplicação da lógica Fuzzy no controle de qualidade na produção de cerveja". Dissertação de Mestrado - Universidade Federal de São João del-Rei. <https://www.ufsj.edu.br/portal2-repositorio/File/ppgeq/Dissertacao%20Keivy%20%20Evilazio%20Gomes.pdf>

4. Silveira, L.F. et al. (2021). "Lógica Fuzzy aplicada ao controle de nível de reservatório de abastecimento de água". *Anais do XV Simpósio Brasileiro de Automação Inteligente*, 15. <https://sba.org.br/open_journal_systems/index.php/sbai/article/view/2690>

5. Carvalho, M.M. et al. (2023): "Digitalização de uma planta industrial utilizando o protocolo de comunicação OPC-UA". *Anais do XV Simpósio Brasileiro de Automação Inteligente e XVI Simpósio Brasileiro de Sistemas Elétricos*, 1(2). <https://www.sba.org.br/open_journal_systems/index.php/sbai/article/view/4056>

6. Petrocchi, G.S. (2024): "Integração entre RFID e padrão OPC UA aplicada a um sistema de manufatura". Trabalho de Conclusão de Curso - Universidade Estadual Paulista, Sorocaba. <https://repositorio.unesp.br/entities/publication/064f9555-efbe-4d75-8703-11dac163ffdf>

7. Silva, M.R.G. (2023): "Instrumentação de uma planta de manufatura flexível utilizando padrão OPC-UA embarcado". Trabalho de Conclusão de Curso - Universidade Federal de Campina Grande. <https://dspace.sti.ufcg.edu.br/xmlui/handle/riufcg/30242>

8. Souza, L.V.F. (2024): "Um estudo de caso sobre controle e supervisão de um processo utilizando o padrão OPC". Trabalho de Conclusão de Curso - Instituto Federal do Espírito Santo. <https://repositorio.ifes.edu.br/handle/123456789/5447>

9. Oliveira Junior, C.M. (2023). "Gerenciamento de nível em reservatório de líquidos por lógica Fuzzy e controle PID". Trabalho de Conclusão de Curso - Universidade Tecnológica Federal do Paraná, Medianeira. <https://repositorio.utfpr.edu.br/jspui/handle/1/33039>

10. Coretti, J.A. (2025). "Automação com redes inteligentes para manutenção de sistemas de controle de processos industriais". Dissertação de Mestrado - Universidade de São Paulo. <https://www.teses.usp.br/teses/disponiveis/55/55134/tde-28042025-203559/>

11. Ogata, K. (2010). "Engenharia de Controle Moderno".  5. ed. São Paulo: Pearson Prentice Hall, 2009. ISBN 9788576056244

12. VILLAÇA, M. V. M.; SILVEIRA, J. L. Uma breve histÓria do controle automÁtico. Revista Ilha Digital, v. 4, p. 3–12, 2013. ISSN 2177-2649. Artigo disponibilizado online.Disponível em: <http://ilhadigital.florianopolis.ifsc.edu.br/>

13. Franklin, G.F.; Powell, J.D.; Emami-Naeini, A. (2015). "Feedback Control of Dynamic Systems". 7th ed. Pearson.

14. Zhou, K.; Doyle, J.C.; Glover, K. (1996). "Robust and Optimal Control". Prentice Hall.

15. Zadeh, L.A. (1965). "Fuzzy Sets". *Information and Control*, 8(3), 338-353.

16. Mamdani, E.H.; Assilian, S. (1975). "An experiment in linguistic synthesis with a fuzzy logic controller". *International Journal of Man-Machine Studies*, 7(1), 1-13.

17. Ross, T.J. (2010). "Fuzzy Logic with Engineering Applications". 3rd ed. John Wiley & Sons.

18. Stenerson, J.; Fixed, K. (2015). "Industrial Automation and Process Control". Prentice Hall.

19. Mahnke, W.; Leitner, S.H.; Damm, M. (2009). "OPC Unified Architecture". Springer.

20. Leitner, S.H.; Mahnke, W. (2006). "OPC UA - Service-oriented Architecture for Industrial Applications". *ABB Review*, 4, 61-66.

21. Jasperneite, J. (2012). "Was hinter Industrie 4.0 steckt". *Computer & Automation*, 19, 24-27.
