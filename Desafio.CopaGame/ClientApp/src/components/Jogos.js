import React, { Component } from 'react';

export class Jogos extends Component {
    static displayName = Jogos.name;

    constructor(props) {
        super(props);
        this.state = { responseJogos: [], loading: true, success: false };
    }

    componentDidMount() {
        this.getDadosJogos();
    }

    static renderListaJogos(jogos) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Ano</th>
                        <th>Titulo</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        jogos.map(jogo =>
                            <tr key={jogo.id}>
                                <td>{jogo.ano}</td>
                                <td>{jogo.titulo}</td>
                            </tr>
                        )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = Jogos.mustRender(this.state)

        return (
            <div>
                <h1 id="tabelLabel" >Jogos da copa</h1>
                <p></p>
                {contents}
            </div>
        );
    }

    static mustRender(state) {
        console.log(state)
        if (state.loading)
            return <p><em>Aguarde...</em></p>
        if (state.success)
            return Jogos.renderListaJogos(state.responseJogos.dados);

        return <p><em>Erro ao carregar</em></p>
    }

    async getDadosJogos() {
        const response = await fetch('api/jogos');
        const data = await response.json();
        this.setState({ responseJogos: data, loading: false, success: data.sucesso });
    }


    async postJogosParticipantes() {
        const jogosCopa =
            [
                {
                    "id": "/nintendo-64/the-legend-of-zelda-ocarina-of-time",
                    "titulo": "The Legend of Zelda: Ocarina of Time (N64)",
                    "nota": 99.9,
                    "ano": 1998,
                    "urlImagem": "https://l3-processoseletivo.azurewebsites.net/api/CapaJogo/nintendo-64/the-legend-of-zelda-ocarina-of-time"
                },
                {
                    "id": "/playstation/tony-hawks-pro-skater-2",
                    "titulo": "Tony Hawk's Pro Skater 2 (PS)",
                    "nota": 98.9,
                    "ano": 2000,
                    "urlImagem": "https://l3-processoseletivo.azurewebsites.net/api/CapaJogo/playstation/tony-hawks-pro-skater-2"
                },
                {
                    "id": "/playstation-3/grand-theft-auto-iv",
                    "titulo": "Grand Theft Auto IV (PS3)",
                    "nota": 98.9,
                    "ano": 2008,
                    "urlImagem": "https://l3-processoseletivo.azurewebsites.net/api/CapaJogo/playstation-3/grand-theft-auto-iv"
                },
                {
                    "id": "/dreamcast/soulcalibur",
                    "titulo": "SoulCalibur (DC)",
                    "nota": 98.9,
                    "ano": 1999,
                    "urlImagem": "https://l3-processoseletivo.azurewebsites.net/api/CapaJogo/dreamcast/soulcalibur"
                },
                {
                    "id": "/xbox-360/grand-theft-auto-iv",
                    "titulo": "Grand Theft Auto IV (X360)",
                    "nota": 98.9,
                    "ano": 2008,
                    "urlImagem": "https://l3-processoseletivo.azurewebsites.net/api/CapaJogo/xbox-360/grand-theft-auto-iv"
                },
                {
                    "id": "/wii/super-mario-galaxy",
                    "titulo": "Super Mario Galaxy (WII)",
                    "nota": 97.9,
                    "ano": 2007,
                    "urlImagem": "https://l3-processoseletivo.azurewebsites.net/api/CapaJogo/wii/super-mario-galaxy"
                },
                {
                    "id": "/wii/super-mario-galaxy-2",
                    "titulo": "Super Mario Galaxy 2 (WII)",
                    "nota": 97.9,
                    "ano": 2010,
                    "urlImagem": "https://l3-processoseletivo.azurewebsites.net/api/CapaJogo/wii/super-mario-galaxy-2"
                },
                {
                    "id": "/xbox-one/red-dead-redemption-2",
                    "titulo": "Red Dead Redemption 2 (XONE)",
                    "nota": 97.9,
                    "ano": 2018,
                    "urlImagem": "https://l3-processoseletivo.azurewebsites.net/api/CapaJogo/xbox-one/red-dead-redemption-2"
                }
            ];
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ jogosCopa })
        };

        fetch('/api/jogos', requestOptions)
            .then(response => response.json())
            .then(data => this.setState({ postId: data.id }));
    }
}
