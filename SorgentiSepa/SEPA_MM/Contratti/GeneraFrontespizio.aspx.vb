Imports System
Imports System.Data
Imports System.IO
Imports Telerik.Web.UI

Partial Class Contratti_GeneraFrontespizio
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", True)
            Exit Sub
        End If

        Me.connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then
            par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM T_MOTIVO_DOMANDA_VSA WHERE COD_PROCESSO_KOFAX IS NOT NULL order by 2 asc", cmbTipoDomanda, "ID", "DESCRIZIONE", True)
        End If

    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "function PaginaHome() {document.location.href = 'pagina_home.aspx';};PaginaHome();", True)
    End Sub

    Protected Sub TextBoxCodRU_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxCodRU.TextChanged
        Try
            If TextBoxCodRU.Text <> "" Then
                par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID,DOMANDE_BANDO_VSA.PG FROM DOMANDE_BANDO_VSA WHERE CONTRATTO_NUM='" & Trim(TextBoxCodRU.Text.ToUpper) & "'" _
                    & " AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=" & cmbTipoDomanda.SelectedValue
                par.caricaComboTelerik(par.cmd.CommandText, cmbPGDomanda, "ID", "PG", False)
            End If
            If cmbPGDomanda.Items.Count > 0 Then
                connData.apri(False)

                par.cmd.CommandText = "SELECT id_dichiarazione,T_MOTIVO_DOMANDA_VSA.COD_PROCESSO_KOFAX FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA WHERE DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID and DOMANDE_BANDO_VSA.id=" & cmbPGDomanda.SelectedValue
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    idDichiarazione.Value = myReader("id_dichiarazione")
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE FROM siscom_mi.RAPPORTI_UTENZA,siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI WHERE RAPPORTI_UTENZA.cod_contratto='" & Trim(TextBoxCodRU.Text.ToUpper) & "'" _
                    & " AND RAPPORTI_UTENZA.id=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL "
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    codUnita.Value = myReader("COD_UNITA_IMMOBILIARE")
                End If
                myReader.Close()
                connData.chiudi(False)
            End If
            'par.cmd.CommandText = "SELECT distinct T_MOTIVO_DOMANDA_VSA.ID,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA WHERE CONTRATTO_NUM='" & Trim(TextBoxCodRU.Text.ToUpper) & "' AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=T_MOTIVO_DOMANDA_VSA.ID"
            'par.caricaComboTelerik(par.cmd.CommandText, cmbTipoDomanda, "ID", "DESCRIZIONE", True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " TextBoxCodRU_TextChanged - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try
    End Sub

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampa.Click
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "Frontespizio();", True)
    End Sub

    Protected Sub cmbPGDomanda_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbPGDomanda.SelectedIndexChanged
        Try
            connData.apri(False)

            par.cmd.CommandText = "SELECT id_dichiarazione,T_MOTIVO_DOMANDA_VSA.COD_PROCESSO_KOFAX FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA WHERE DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID and DOMANDE_BANDO_VSA.id=" & cmbPGDomanda.SelectedValue
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                idDichiarazione.Value = myReader("id_dichiarazione")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE FROM siscom_mi.RAPPORTI_UTENZA,siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI WHERE RAPPORTI_UTENZA.cod_contratto='" & Trim(TextBoxCodRU.Text.ToUpper) & "'" _
                & " AND RAPPORTI_UTENZA.id=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL "
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                codUnita.Value = myReader("COD_UNITA_IMMOBILIARE")
            End If
            myReader.Close()

            connData.chiudi(False)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " cmbPGDomanda_SelectedIndexChanged - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try
    End Sub

    Protected Sub cmbTipoDomanda_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbTipoDomanda.SelectedIndexChanged
        Try
            connData.apri(False)

            par.cmd.CommandText = "SELECT T_MOTIVO_DOMANDA_VSA.COD_PROCESSO_KOFAX FROM T_MOTIVO_DOMANDA_VSA WHERE T_MOTIVO_DOMANDA_VSA.id=" & cmbTipoDomanda.SelectedValue
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                cod_kofax.Value = myReader("COD_PROCESSO_KOFAX")
            End If
            myReader.Close()

            connData.chiudi(False)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " cmbTipoDomanda_SelectedIndexChanged - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Errore", "location.replace('../Errore.aspx');", True)
        End Try
    End Sub
End Class
