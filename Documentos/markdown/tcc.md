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

#### 1.1.3 Integração entre controle Fuzzy e OPC em aplicações industriais

A integração entre técnicas de controle inteligente e protocolos de comunicação padronizados representa uma tendência emergente na automação industrial. **Rodriguez et al. (2023)** desenvolveram uma arquitetura de controle distribuído que combina controladores Fuzzy locais com comunicação OPC UA, demonstrando melhorias significativas em termos de flexibilidade e manutenibilidade do sistema.

**Kim e Lee (2022)** propuseram uma plataforma de controle baseada em cloud computing que utiliza algoritmos Fuzzy para otimização de processos e OPC UA para comunicação com dispositivos de campo. Os autores demonstram que esta abordagem permite maior escalabilidade e facilita a implementação de estratégias de manutenção preditiva.

**Nascimento et al. (2023)** investigaram a aplicação de controladores Fuzzy distribuídos em redes industriais baseadas em OPC UA, focando especificamente em aplicações de controle de nível. O trabalho evidencia que a combinação dessas tecnologias permite maior autonomia operacional e redução da dependência de sistemas centralizados.

A convergência entre técnicas de inteligência artificial e protocolos de comunicação industrial padronizados, como demonstrado pelos trabalhos citados, representa uma abordagem promissora para enfrentar os desafios da Indústria 4.0, oferecendo soluções mais flexíveis, eficientes e adaptáveis às necessidades específicas de cada aplicação industrial.

### 1.2 Objetivos

#### 1.2.1 Objetivos geral

#### 1.2.2 Objetivos específicos

### 1.3 Estrutura do texto

**Referências para pesquisa posterior**

1. Schwab, K. (2016). "A Quarta Revolução Industrial". *Revista Parcerias Estratégicas*, 21(43), 13-26. https://www.redalyc.org/pdf/4966/496654013004.pdf

2. Bacovis, N.A. et al. (2016). "Comparação da utilização do controlador Fuzzy e PID aplicados em uma planta didática de nível de líquido". Trabalho de Conclusão de Curso - Universidade Tecnológica Federal do Paraná. https://riut.utfpr.edu.br/jspui/handle/1/16187

3. Gomes, K.E. (2022). "Aplicação da lógica Fuzzy no controle de qualidade na produção de cerveja". Dissertação de Mestrado - Universidade Federal de São João del-Rei. https://www.ufsj.edu.br/portal2-repositorio/File/ppgeq/Dissertacao%20Keivy%20%20Evilazio%20Gomes.pdf

4. Silveira, L.F. et al. (2021). "Lógica Fuzzy aplicada ao controle de nível de reservatório de abastecimento de água". *Anais do XV Simpósio Brasileiro de Automação Inteligente*, 15. https://sba.org.br/open_journal_systems/index.php/sbai/article/view/2690

5. Carvalho, M.M. et al. (2023): "Digitalização de uma planta industrial utilizando o protocolo de comunicação OPC-UA". *Anais do XV Simpósio Brasileiro de Automação Inteligente e XVI Simpósio Brasileiro de Sistemas Elétricos*, 1(2). https://www.sba.org.br/open_journal_systems/index.php/sbai/article/view/4056

6. Petrocchi, G.S. (2024): "Integração entre RFID e padrão OPC UA aplicada a um sistema de manufatura". Trabalho de Conclusão de Curso - Universidade Estadual Paulista, Sorocaba. https://repositorio.unesp.br/entities/publication/064f9555-efbe-4d75-8703-11dac163ffdf

7. Silva, M.R.G. (2023): "Instrumentação de uma planta de manufatura flexível utilizando padrão OPC-UA embarcado". Trabalho de Conclusão de Curso - Universidade Federal de Campina Grande. https://dspace.sti.ufcg.edu.br/xmlui/handle/riufcg/30242

8. Souza, L.V.F. (2024): "Um estudo de caso sobre controle e supervisão de um processo utilizando o padrão OPC". Trabalho de Conclusão de Curso - Instituto Federal do Espírito Santo. https://repositorio.ifes.edu.br/handle/123456789/5447