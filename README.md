# Projeto para PDI sobre SNMP

# Passo a Passo para Instalar e Ativar o Serviço SNMP no Windows

## 1. Abrir o "Painel de Controle"

- Clique no botão **Iniciar**.
- Digite "Painel de Controle" na barra de pesquisa e pressione **Enter**.

## 2. Acessar "Programas"

- Dentro do **Painel de Controle**, clique em **Programas**.
- Em seguida, clique em **Ativar ou desativar recursos do Windows**.

## 3. Instalar o SNMP

- Na janela **Recursos do Windows**, role para baixo e localize a opção **Protocolo Simple Network Management (SNMP)**.
- Marque a caixa de seleção ao lado de **Simple Network Management Protocol (SNMP)**.
- Clique em **OK** para iniciar a instalação.
- O sistema pode solicitar a instalação de outros componentes necessários; confirme a instalação.

## 4. Ativar o Serviço SNMP

### 4.1 Acessar "Serviços"

- Abra o menu **Iniciar** novamente e digite **services.msc**. Pressione **Enter**.
- Isso abrirá a janela **Serviços**.

### 4.2 Localizar o Serviço SNMP

- Na janela **Serviços**, localize o serviço chamado **Agente SNMP** (ou **SNMP Service**).
- Clique com o botão direito sobre o serviço e selecione **Propriedades**.

### 4.3 Configurar o Serviço

- Na aba **Geral**, altere o tipo de inicialização para **Automático** para que o serviço inicie automaticamente com o Windows.
- Clique em **Iniciar** para iniciar o serviço imediatamente.
- Clique em **OK** para salvar as configurações.

## 5. Configurar Comunidade SNMP

### 5.1 Acessar Configurações SNMP

- Volte para a janela **Propriedades** do **Agente SNMP**.
- Vá para a aba **Segurança**.

### 5.2 Adicionar Comunidade

- Clique em **Adicionar** para configurar a **comunidade** SNMP.
- Insira um nome para a comunidade (exemplo: "public") e defina as permissões.
- Clique em **Adicionar** para salvar a configuração.

## 6. Testar a Configuração

- Utilize uma ferramenta de monitoramento SNMP, como o **SNMPwalk** ou **Paessler SNMP Tester**, para verificar se o SNMP está funcionando corretamente no seu PC.
