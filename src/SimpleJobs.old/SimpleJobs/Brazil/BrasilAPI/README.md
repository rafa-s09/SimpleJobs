# BrasilAPI (EN)
[Official Site](https://brasilapi.com.br/)

## Developer Notes

> This class is a simplified abstraction of BrasilAPI, we ask you to visit the project's website and carefully read the terms of use<br/>

## Reason 

Programmatic access to information is fundamental in communication between systems but, to our surprise, information as useful and public as a zip code cannot be accessed directly by a browser because the Correios API does not have CORS enabled, so the objective of this project is to facilitate the consultation of all this data.

## Terms of use

BrasilAPI is an initiative made by Brazilians for Brazilians, please **do not abuse this service.** We are in beta and still working on the Terms of Use, but for now please **do not use automated ways to crawl or full scan** the API data.<br/>
**Never do:** Loop requests. (For example zip codes from 00000000 99999999)<br/>
A practical example of this was when one of the largest telephony providers in Brazil was revalidating all zip codes (from 00000000 to 99999999 ) and exceeding the current limit of our account on the server by 5 times. The consultation volume must have the nature of a real person requesting a certain data. And for queries with a high automated volume, we will later provide some solution, such as being able to download the entire zip code base in a single request.<br/>
<br/>


# BrasilAPI (Pt-Br)
[Site Official](https://brasilapi.com.br/)

## Notas do Desenvolvedor

> Esta classe é uma abstração simplificada da BrasilAPI, pedimos que visite o site do projeto e leia atentamente os termos de uso.

## Motivo

Acesso programático de informações é algo fundamental na comunicação entre sistemas mas, para nossa surpresa, uma informação tão útil e pública quanto um CEP não consegue ser acessada diretamente por um navegador por conta da API dos Correios não possuir CORS habilitado, então o objetivo desse projeto é facilitar a consulta de todos esses dados.

## Termos de Uso

O BrasilAPI é uma iniciativa feita de brasileiros para brasileiros, por favor, **não abuse deste serviço**. Estamos em beta e ainda elaborando os Termos de Uso, mas por enquanto por favor **não utilize formas automatizadas para fazer crawling ou full scan** dos dados da API.<br/>
**Nunca faça:** Requisições em loop. (Por exemplo ceps de 00000000 99999999) <br/>
Um exemplo prático disto foi quando dos maiores provedores de telefonia do Brasil estava revalidando, todos os ceps (de 00000000 até 99999999 ) e estourando em 5 vezes o limite atual da nossa conta no servidor. O volume de consulta dever ter a natureza de uma pessoa real requisitando um determinado dado. E para consultas com um alto volume automatizado, iremos mais para frente fornecer alguma solução, como por exemplo, conseguir fazer o download de toda a base de ceps em uma única request.
