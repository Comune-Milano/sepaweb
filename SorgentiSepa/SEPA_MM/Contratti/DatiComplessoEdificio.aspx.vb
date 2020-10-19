
Partial Class Contratti_DatiComplessoEdificio
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Label1.Text = "Codice Unità: " & Request.QueryString("COD")
            par.cmd.CommandText = "select EDIFICI.CONDOMINIO,EDIFICI.cod_livello_possesso,tipologia_imp_riscaldamento.descrizione as tiporiscaldamento,tipologia_costruttiva.descrizione as tipologiacostruttiva,tipologia_edificio.descrizione as tipologiaedificio,utilizzo_principale_edificio.descrizione as utilizzoedificio,edifici.denominazione as denominazioneedificio,edifici.cod_edificio,tab_filiali.nome as filiale,tab_commissariati.descrizione as commissariato,tipologia_provenienza.descrizione as tipologiaprovenienza,complessi_immobiliari.lotto,tipo_ubicazione_lg_392_78.descrizione as tipoubicazione,TIPO_COMPLESSO_IMMOBILIARE.DESCRIZIONE AS TIPOCOMPLESSO,livello_possesso.descrizione as livpossesso,complessi_immobiliari.lotto,complessi_immobiliari.cod_complesso,complessi_immobiliari.denominazione from siscom_mi.tab_filiali,siscom_mi.tab_commissariati,siscom_mi.tipologia_provenienza,siscom_mi.tipo_ubicazione_lg_392_78,SISCOM_MI.TIPO_COMPLESSO_IMMOBILIARE,siscom_mi.livello_possesso,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari,siscom_mi.utilizzo_principale_edificio,siscom_mi.tipologia_edificio,SISCOM_MI.tipologia_costruttiva,siscom_mi.tipologia_imp_riscaldamento where tipologia_imp_riscaldamento.cod=edifici.cod_tipologia_imp_riscald (+) and edifici.cod_tipologia_costruttiva=tipologia_costruttiva.cod (+) and edifici.cod_tipologia_edificio=tipologia_edificio.cod (+) and  edifici.cod_utilizzo_principale=utilizzo_principale_edificio.cod (+) and complessi_immobiliari.id_filiale=tab_filiali.id (+) and  complessi_immobiliari.id_commissariato=tab_commissariati.id (+) and  tipologia_provenienza.cod=complessi_immobiliari.cod_tipologia_provenienza and tipo_ubicazione_lg_392_78.cod=complessi_immobiliari.cod_tipo_ubicazione_lg_392_78 AND TIPO_COMPLESSO_IMMOBILIARE.COD=COMPLESSI_IMMOBILIARI.COD_TIPO_COMPLESSO AND livello_possesso.cod=complessi_immobiliari.cod_livello_possesso and complessi_immobiliari.id=edifici.id_complesso and edifici.id=unita_immobiliari.id_edificio and unita_immobiliari.cod_unita_immobiliare='" & Request.QueryString("COD") & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblcodicecomplesso.Text = par.IfNull(myReader("cod_complesso"), "")
                lblDenominazione.Text = par.IfNull(myReader("denominazione"), "")
                lbllivellopossesso.Text = par.IfNull(myReader("livpossesso"), "")
                lbltipocomplesso.Text = par.IfNull(myReader("TIPOCOMPLESSO"), "")
                lblubicazione.Text = par.IfNull(myReader("tipoubicazione"), "")
                lbllotto.Text = par.IfNull(myReader("lotto"), "")
                lblprovenienza.Text = par.IfNull(myReader("tipologiaprovenienza"), "")
                lblcommissariato.Text = par.IfNull(myReader("commissariato"), "")
                lblfiliale.Text = par.IfNull(myReader("filiale"), "")

                'edificio
                lbldenominazioneedificio.Text = par.IfNull(myReader("denominazioneedificio"), "")
                lblcodiceedificio.Text = par.IfNull(myReader("cod_edificio"), "")
                lbltipologiaedificio.Text = par.IfNull(myReader("tipologiaedificio"), "")
                lblutilizzoedificio.Text = par.IfNull(myReader("utilizzoedificio"), "")
                lbltipologiacostruttiva.Text = par.IfNull(myReader("tipologiacostruttiva"), "")
                lblpossessoedificio.Text = par.IfNull(myReader("cod_livello_possesso"), "")

                If par.IfNull(myReader("condominio"), "") = "0" Then
                    lblcondominio.Text = "NO"
                Else
                    lblcondominio.Text = "SI"
                End If
                lblriscaldamento.Text = par.IfNull(myReader("tiporiscaldamento"), "")


            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
