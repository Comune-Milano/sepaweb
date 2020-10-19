
Partial Class Contratti_Report_P_SingoleVociNon
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        


        Try
            Dim S As String = ""

            Dim StringaSQL As String = "SELECT  T_VOCI_BOLLETTA.ID,T_VOCI_BOLLETTA.DESCRIZIONE , TO_CHAR (SUM(BOL_BOLLETTE_VOCI.IMPORTO-NVL(BOL_BOLLETTE_VOCI.IMP_PAGATO,0)), '9G999G999G999G990D99') AS IMPORTO,'' as DETTAGLI FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NULL AND (BOL_BOLLETTE.FL_ANNULLATA='0' OR (BOL_BOLLETTE.FL_ANNULLATA<>'0' AND DATA_PAGAMENTO IS NOT NULL )) and fl_stampato='1' and T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE_VOCI.IMPORTO<>NVL(BOL_BOLLETTE_VOCI.IMP_PAGATO,0)  "
            Dim StringaSQLAnnullate As String = "SELECT distinct BOL_BOLLETTE.ID FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NULL AND BOL_BOLLETTE.FL_ANNULLATA<>'0' and fl_stampato='1' AND BOL_BOLLETTE.DATA_PAGAMENTO IS NULL AND T_VOCI_BOLLETTA.ID = ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND BOL_BOLLETTE_VOCI.IMPORTO<>NVL(BOL_BOLLETTE_VOCI.IMP_PAGATO,0) "


            If Me.RdbTipologia.SelectedValue = "Attiva" Then
                StringaSQL = StringaSQL & " AND (BOL_BOLLETTE.RIF_FILE='MOD' OR BOL_BOLLETTE.RIF_FILE='MAV') "
                StringaSQLAnnullate = StringaSQLAnnullate & " AND (BOL_BOLLETTE.RIF_FILE='MOD' OR BOL_BOLLETTE.RIF_FILE='MAV') "
            End If

            If Me.RdbTipologia.SelectedValue = "Bollettazione" Then
                StringaSQL = StringaSQL & " AND BOL_BOLLETTE.RIF_FILE<>'MOD' and BOL_BOLLETTE.RIF_FILE<>'MAV' and BOL_BOLLETTE.RIF_FILE<>'REC' "
                StringaSQLAnnullate = StringaSQLAnnullate & " AND BOL_BOLLETTE.RIF_FILE<>'MOD' and BOL_BOLLETTE.RIF_FILE<>'MAV' and BOL_BOLLETTE.RIF_FILE<>'REC' "
            End If

            If Me.RdbTipologia.SelectedValue = "Virt.Manuale" Then
                StringaSQL = StringaSQL & " AND BOL_BOLLETTE.RIF_FILE='REC' "
                StringaSQLAnnullate = StringaSQLAnnullate & " AND BOL_BOLLETTE.RIF_FILE='REC' "
            End If


            '*********SEZIONE INERENTE ALLE DATE

            If par.IfEmpty(Me.txtDataDal.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND DATA_EMISSIONE>= " & par.AggiustaData(Me.txtDataDal.Text)
                StringaSQLAnnullate = StringaSQLAnnullate & " AND DATA_EMISSIONE>= " & par.AggiustaData(Me.txtDataDal.Text)
            End If

            If par.IfEmpty(Me.txtDataAl.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND DATA_EMISSIONE<= " & par.AggiustaData(Me.txtDataAl.Text)
                StringaSQLAnnullate = StringaSQLAnnullate & " AND DATA_EMISSIONE<= " & par.AggiustaData(Me.txtDataAl.Text)
            End If

            If par.IfEmpty(Me.txtDataDal0.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND DATA_pagamento>= " & par.AggiustaData(Me.txtDataDal0.Text)
                StringaSQLAnnullate = StringaSQLAnnullate & " AND DATA_pagamento>= " & par.AggiustaData(Me.txtDataDal0.Text)
            End If

            If par.IfEmpty(Me.txtDataAl0.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND DATA_pagamento<= " & par.AggiustaData(Me.txtDataAl0.Text)
                StringaSQLAnnullate = StringaSQLAnnullate & " AND DATA_pagamento<= " & par.AggiustaData(Me.txtDataAl0.Text)
            End If



            If par.IfEmpty(Me.txtRifDal.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND RIFERIMENTO_DA>= " & par.AggiustaData(Me.txtRifDal.Text)
                StringaSQLAnnullate = StringaSQLAnnullate & " AND RIFERIMENTO_DA>= " & par.AggiustaData(Me.txtRifDal.Text)
            End If

            If par.IfEmpty(Me.txtRifAl.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND RIFERIMENTO_A<= " & par.AggiustaData(Me.txtRifAl.Text)
                StringaSQLAnnullate = StringaSQLAnnullate & " AND RIFERIMENTO_A<= " & par.AggiustaData(Me.txtRifAl.Text)
            End If

            'DATA VALUTA
            If par.IfEmpty(Me.txtDataDal1.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND DATA_VALUTA>= " & par.AggiustaData(Me.txtDataDal1.Text)
                StringaSQLAnnullate = StringaSQLAnnullate & " AND DATA_VALUTA>= " & par.AggiustaData(Me.txtDataDal1.Text)
            End If

            If par.IfEmpty(Me.txtDataAl1.Text, "Null") <> "Null" Then
                StringaSQL = StringaSQL & " AND DATA_VALUTA<= " & par.AggiustaData(Me.txtDataAl1.Text)
                StringaSQLAnnullate = StringaSQLAnnullate & " AND DATA_VALUTA<= " & par.AggiustaData(Me.txtDataAl1.Text)
            End If

            '*********FINE SEZIONE INERENTE ALLE DATE

            If Selezionati = "1" Then
                StringaSQL = StringaSQL & " AND ("
                StringaSQLAnnullate = StringaSQLAnnullate & " AND ("
                Dim i As Integer = 0
                Dim primo As Boolean = False
                For Each o As Object In CheckVociBoll.Items
                    Dim item As System.Web.UI.WebControls.ListItem
                    item = CType(o, System.Web.UI.WebControls.ListItem)
                    If item.Selected Then
                        If primo = False Then
                            S = " ID_VOCE =" & item.Value
                            primo = True
                        Else
                            S = S & " OR ID_VOCE =" & item.Value
                        End If

                    End If
                Next
                StringaSQL = StringaSQL & S & ")"
                StringaSQLAnnullate = StringaSQLAnnullate & S & ")"
            Else

                StringaSQL = StringaSQL & " AND ("
                StringaSQLAnnullate = StringaSQLAnnullate & " AND ("

                Dim i As Integer = 0
                Dim primo As Boolean = False
                For Each o As Object In CheckVociBoll.Items
                    Dim item As System.Web.UI.WebControls.ListItem
                    item = CType(o, System.Web.UI.WebControls.ListItem)
                    If primo = False Then
                        S = " ID_VOCE <>" & item.Value
                        primo = True
                    Else
                        S = S & " and ID_VOCE <>" & item.Value
                    End If

                Next
                StringaSQL = StringaSQL & S & ")"
                StringaSQLAnnullate = StringaSQLAnnullate & S & ")"

            End If

            If Selezionati <> "1" Then
                Response.Write("<script>alert('Selezionare almeno una voce!');</script>")
                Exit Sub
            End If

            Session.Add("REPORT", StringaSQL)
            Session.Add("REPORT_ANNULLATE", StringaSQLAnnullate)

            'Response.Write("<script>window.open('StampaP_SingoleVoci.aspx?O=1&RIFDAL1=" & par.AggiustaData(Me.txtDataDal1.Text) & "&RIFAL1=" & par.AggiustaData(Me.txtDataAl1.Text) & "&RIFDAL=" & par.AggiustaData(Me.txtRifDal.Text) & "&RIFAL=" & par.AggiustaData(Me.txtRifAl.Text) & "&DAL0=" & par.AggiustaData(Me.txtDataDal0.Text) & "&AL0=" & par.AggiustaData(Me.txtDataAl0.Text) & "&DAL=" & par.AggiustaData(Me.txtDataDal.Text) & "&AL=" & par.AggiustaData(Me.txtDataAl.Text) & "&TIPO=" & Me.RdbTipologia.SelectedValue.ToString & "');</script>")

            Response.Write("<script>window.open('StampaP1_SingoleVociNon.aspx?O=1&RIFDAL1=" & par.AggiustaData(Me.txtDataDal1.Text) & "&RIFAL1=" & par.AggiustaData(Me.txtDataAl1.Text) & "&RIFDAL=" & par.AggiustaData(Me.txtRifDal.Text) & "&RIFAL=" & par.AggiustaData(Me.txtRifAl.Text) & "&DAL0=" & par.AggiustaData(Me.txtDataDal0.Text) & "&AL0=" & par.AggiustaData(Me.txtDataAl0.Text) & "&DAL=" & par.AggiustaData(Me.txtDataDal.Text) & "&AL=" & par.AggiustaData(Me.txtDataAl.Text) & "&TIPO=" & Me.RdbTipologia.SelectedValue.ToString & "');</script>")


        Catch ex As Exception
            Me.lblErrore.Visible = True

            lblErrore.Text = ex.Message

        End Try
    End Sub

    Public Property Selezionati() As String
        Get
            If Not (ViewState("par_Selezionati") Is Nothing) Then
                Return CStr(ViewState("par_Selezionati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Selezionati") = value
        End Set

    End Property

    Private Sub CaricaVociBolletta()
        Try
            If HiddenField1.Value = "0" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Me.CheckVociBoll.Items.Clear()

                'If Me.RdbTipologia.SelectedValue = "Generale" Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_VOCI_BOLLETTA  ORDER BY DESCRIZIONE ASC"
                'Else
                'par.cmd.CommandText = "SELECT DISTINCT  T_VOCI_BOLLETTA.ID, DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA  WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA  IN (SELECT ID FROM SISCOM_MI.BOL_BOLLETTE WHERE N_RATA = 99) AND BOL_BOLLETTE_VOCI.ID_VOCE = T_VOCI_BOLLETTA.ID "
                'End If

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    CheckVociBoll.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
                End While
                myReader.Close()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                HiddenField1.Value = "1"
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Function Seleziona()
        If Selezionati = "" Then
            Selezionati = 1
        Else
            Selezionati = ""
        End If
        Dim a As Integer
        Dim i As Integer = 0
        If Selezionati <> "" Then
            a = CheckVociBoll.Items.Count.ToString
            While i < a
                Me.CheckVociBoll.Items(i).Selected = True
                i = i + 1
            End While
        Else
            a = CheckVociBoll.Items.Count.ToString
            While i < a
                Me.CheckVociBoll.Items(i).Selected = False
                i = i + 1
            End While
        End If
    End Function

    Protected Sub btnSelezionaTutto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelezionaTutto.Click
        Seleziona()
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Contabilita/pagina_home.aspx""</script>")
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.RdbTipologia.SelectedValue = "Generale"
            CaricaVociBolletta()
            Seleziona()

        End If
        txtDataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtRifDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtRifAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataDal0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataDal1.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataAl1.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub CheckVociBoll_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckVociBoll.SelectedIndexChanged
        Selezionati = 1
    End Sub

    Protected Sub RdbTipologia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdbTipologia.SelectedIndexChanged
        CaricaVociBolletta()
    End Sub

    Protected Sub ChComune_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChComune.CheckedChanged
        If ChComune.Checked = True Then
            SelezionaTuttoComune()
            CheckVociBoll.Enabled = False
            btnSelezionaTutto.Enabled = False
        Else
            CheckVociBoll.Enabled = True
            btnSelezionaTutto.Enabled = True
        End If
    End Sub

    Private Function SelezionaTuttoComune()

        Selezionati = 1

        Dim a As Integer
        Dim i As Integer = 0

        a = CheckVociBoll.Items.Count.ToString
        While i < a
            If Me.CheckVociBoll.Items(i).Value = 96 Or Me.CheckVociBoll.Items(i).Value = 7 Then
                Me.CheckVociBoll.Items(i).Selected = False
            Else
                Me.CheckVociBoll.Items(i).Selected = True
            End If
            i = i + 1
        End While

    End Function
End Class
