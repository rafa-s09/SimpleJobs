# BrasilAPI
[Site Official](https://brasilapi.com.br/)

## Notas do Desenvolvedor

> Esta classe é uma abstração simplificada da BrasilAPI, pedimos que visite o site do projeto e leia atentamente os termos de uso.

## Motivo

Acesso programático de informações é algo fundamental na comunicação entre sistemas mas, para nossa surpresa, uma informação tão útil e pública quanto um CEP não consegue ser acessada diretamente por um navegador por conta da API dos Correios não possuir CORS habilitado, então o objetivo desse projeto é facilitar a consulta de todos esses dados.

## Termos de Uso

O BrasilAPI é uma iniciativa feita de brasileiros para brasileiros, por favor, **não abuse deste serviço**. Estamos em beta e ainda elaborando os Termos de Uso, mas por enquanto por favor **não utilize formas automatizadas para fazer crawling ou full scan** dos dados da API.<br/>
**Nunca faça:** Requisições em loop. (Por exemplo ceps de 00000000 99999999) <br/>
Um exemplo prático disto foi quando dos maiores provedores de telefonia do Brasil estava revalidando, todos os ceps (de 00000000 até 99999999 ) e estourando em 5 vezes o limite atual da nossa conta no servidor. O volume de consulta dever ter a natureza de uma pessoa real requisitando um determinado dado. E para consultas com um alto volume automatizado, iremos mais para frente fornecer alguma solução, como por exemplo, conseguir fazer o download de toda a base de ceps em uma única request.
