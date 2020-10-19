
Partial Class CENSIMENTO_Tab_UnComuni
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String = ""

    Public Property vId() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            vId = CType(Me.Page, Object).vId
            CaricaUnitaComuni()
        End If
        '**********SERVE PER RECUPERARE ID SUBITO DOPO NUOVO INSERIMENTO IMMOBILE*****************
        If vId = 0 Then
            vId = CType(Me.Page, Object).vId
        End If
        '**********************FINE MODIFICA PER ID NUOVO INSERMENTO******************************
    End Sub
    Public Sub CaricaUnitaComuni()
        Try
            If Session("SLE") = "1" Then
                par.OracleConn.Open()
                par.SettaCommand(par)

            Else
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            End If

            If Me.Page.ToString.ToUpper.Contains("COMPLESSI") Then
                sStringaSql = "SELECT ROWNUM, SISCOM_MI.UNITA_COMUNI.ID, UNITA_COMUNI.COD_UNITA_COMUNE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE, INDIRIZZI.CIVICO, COMUNI_NAZIONI.NOME, (TIPO_UNITA_COMUNE.DESCRIZIONE) AS TIPOLOGIA FROM SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.UNITA_COMUNI.ID_COMPLESSO and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = SISCOM_MI.INDIRIZZI.ID (+) AND SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+) AND UNITA_COMUNI.COD_TIPOLOGIA = TIPO_UNITA_COMUNE.COD AND ID_COMPLESSO =" & vId
            Else
                sStringaSql = "SELECT ROWNUM, SISCOM_MI.UNITA_COMUNI.ID, UNITA_COMUNI.COD_UNITA_COMUNE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE, INDIRIZZI.CIVICO, COMUNI_NAZIONI.NOME, (TIPO_UNITA_COMUNE.DESCRIZIONE) AS TIPOLOGIA FROM SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI WHERE SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.UNITA_COMUNI.ID_COMPLESSO and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = SISCOM_MI.INDIRIZZI.ID (+) AND SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+) AND UNITA_COMUNI.COD_TIPOLOGIA = TIPO_UNITA_COMUNE.COD AND ID_COMPLESSO =" & vId

            End If


            par.cmd.CommandText = sStringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "EDIFICI_COMPLESSI")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            If Session("SLE") = "1" Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Not IsNothing(txtid.Value) And txtid.Value <> 0 Then
            If vId <> -1 Then
                If CType(Me.Page.FindControl("txtModificato"), HiddenField).Value <> "111" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.OracleConn.Close()

                    HttpContext.Current.Session.Remove("TRANSAZIONE")
                    HttpContext.Current.Session.Remove("CONNESSIONE")
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Session.Item("LAVORAZIONE") = 0
                    If Me.Page.ToString.ToUpper.Contains("COMPLESSI") Then
                        'Response.Redirect("UnitàComEdifici.aspx?C=InserimentoComplessi&IDC=" & vId & "&PAS=COM&ID=" & vId)
                        Response.Redirect("UnitàComEdifici.aspx?C=InserimentoComplessi&IDC=" & vId & "&PAS=COM&ID=" & Me.txtid.Value)

                    Else
                        Response.Redirect("UnitàComEdifici.aspx?C=InserimentoEdifici&IDE=" & vId & "&PAS=EDI")

                    End If
                    Me.txtid.Value = 0
                    Me.txtmia.Text = ""
                Else
                    CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = "1"
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                End If

            End If

        Else
            Response.Write("<script>alert('Nessuna Unità selezionata!')</script>")
        End If
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_UnComuni1_txtmia').value='Hai selezionato l\'unita con cod. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_UnComuni1_txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_UnComuni1_txtmia').value='Hai selezionato l\'unita con cod. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_UnComuni1_txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub


    Protected Sub btnNuovoUC_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNuovoUC.Click
        If CType(Me.Page.FindControl("txtModificato"), HiddenField).Value <> "111" Then
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            HttpContext.Current.Session.Remove("TRANSAZIONE")
            HttpContext.Current.Session.Remove("CONNESSIONE")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = 0

            If Me.Page.ToString.ToUpper.Contains("COMPLESSI") Then
                Response.Redirect("UnitàComEdifici.aspx?C=InserimentoComplessi&RICPER=COM&IDC=" & vId & "&COMPEDI=" & vId)
            Else
                Response.Redirect("UnitàComEdifici.aspx?C=InserimentoComplessi&RICPER=ED&IDED=" & vId & "&COMPEDI=" & vId)

            End If
        Else
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub
End Class
