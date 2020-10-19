
Partial Class Contabilita_EstrattoConto
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        TxtDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtRifAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtRifDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtIncDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtIncAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtValutaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtValutaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        If Not IsPostBack Then
            tipoEstratto.Value = Request.QueryString("T")
            If Request.QueryString("T") = "G" Then
                lbltipologia.Visible = True
                cblTipologia.Visible = True
                btntipologia.Visible = True
                CaricaTipoDoc()
                lblTipoEstratto.Text = "Gestionale"
                btnVisualizza.Visible = False
                btnAnnulla.Visible = False
                lblNota.Visible = True
            Else
                lblTipoEstratto.Text = "Contabile"
                btnVisualizza2.Visible = False
                btnAnnulla2.Visible = False
                lblNota.Visible = False
            End If
        End If
    End Sub

    Private Sub CaricaTipoDoc()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand
            par.cmd.CommandText = "SELECT * from SISCOM_MI.TIPO_BOLLETTE_GEST WHERE FL_VISUALIZZABILE=1 ORDER BY DESCRIZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            'For Each row As Data.DataRow In dt.Rows
            '    cblTipologia.Items.Add(New ListItem(par.IfNull(row.Item("descrizione"), " "), par.IfNull(row.Item("id"), -1)))
            'Next



            cblTipologia.DataSource = dt
            cblTipologia.DataValueField = "ID"
            cblTipologia.DataTextField = "DESCRIZIONE"
            cblTipologia.DataBind()
            For Each ch As ListItem In cblTipologia.Items
                If ch.Value = 4 Then
                    ch.Attributes.Add("onclick", "javascript:controllaTipoCheck('" & ch.Value & "');")
                End If

            Next

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza2.Click

        '******controllo che la data iniziale sia avvalorata e che se avvalorata anche quella finale questa non sia minore di quella iniziale altimenti
        '******l'estratto conto generato non averebbe alcun senso!
        If conferma.Value = "1" Then
            If par.IfEmpty(Me.TxtDal.Text, "Null") <> "Null" Then
                If par.IfEmpty(Me.TxtAl.Text, "Null") <> "Null" Then
                    If par.AggiustaData(Me.TxtAl.Text) < par.AggiustaData(Me.TxtDal.Text) Then
                        Response.Write("<script>alert('Definire correttamente l\'intervallo di tempo!')</script>")
                        Exit Sub
                    Else
                        Response.Write("<script>window.open('EstrattoConto_Gest.aspx?DAL=" & par.AggiustaData(Me.TxtDal.Text) & "&AL=" & par.AggiustaData(Me.TxtAl.Text) & "&RIFDAL=" & par.AggiustaData(Me.TxtRifDal.Text) & "&RIFAL=" & par.AggiustaData(Me.TxtRifAl.Text) & "&INCDAL=" & par.AggiustaData(Me.txtIncDal.Text) & "&INCAL=" & par.AggiustaData(Me.txtIncAl.Text) & "&VALDAL=" & par.AggiustaData(Me.txtValutaDal.Text) & "&VALAL=" & par.AggiustaData(Me.txtValutaAl.Text) & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "&TIPO=" & SceltaCheck(cblTipologia) & "','Estratto', '');</script>")
                    End If
                End If
                'Else
                '    Response.Write("<script>alert('Definire almeno la data iniziale di emissione!')</script>")
                '    Exit Sub
            End If
            Response.Write("<script>window.open('EstrattoConto_Gest.aspx?DAL=" & par.AggiustaData(Me.TxtDal.Text) & "&AL=" & par.AggiustaData(Me.TxtAl.Text) & "&RIFDAL=" & par.AggiustaData(Me.TxtRifDal.Text) & "&RIFAL=" & par.AggiustaData(Me.TxtRifAl.Text) & "&INCDAL=" & par.AggiustaData(Me.txtIncDal.Text) & "&INCAL=" & par.AggiustaData(Me.txtIncAl.Text) & "&VALDAL=" & par.AggiustaData(Me.txtValutaDal.Text) & "&VALAL=" & par.AggiustaData(Me.txtValutaAl.Text) & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "&TIPO=" & SceltaCheck(cblTipologia) & "','Estratto', '');</script>")
        End If

    End Sub

    Private Function SceltaCheck(ByRef checkboxlist As CheckBoxList) As String
        Dim StringaCheck As String = ""
        For Each Items As ListItem In checkboxlist.Items
            If Items.Selected = True Then
                StringaCheck = StringaCheck & Items.Value & ","
            End If
        Next
        If StringaCheck <> "" Then
            StringaCheck = Left(StringaCheck, Len(StringaCheck) - 1)
        End If
        Return StringaCheck
    End Function

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla2.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub btnAnnulla_Click1(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub btnVisualizza_Click1(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If conferma.Value = "1" Then
            If par.IfEmpty(Me.TxtDal.Text, "Null") <> "Null" Then
                If par.IfEmpty(Me.TxtAl.Text, "Null") <> "Null" Then
                    If par.AggiustaData(Me.TxtAl.Text) < par.AggiustaData(Me.TxtDal.Text) Then
                        Response.Write("<script>alert('Definire correttamente l\'intervallo di tempo!')</script>")
                        Exit Sub
                    Else
                        Response.Write("<script>location.replace('EstrattoConto_New.aspx?DAL=" & par.AggiustaData(Me.TxtDal.Text) & "&AL=" & par.AggiustaData(Me.TxtAl.Text) & "&RIFDAL=" & par.AggiustaData(Me.TxtRifDal.Text) & "&RIFAL=" & par.AggiustaData(Me.TxtRifAl.Text) & "&INCDAL=" & par.AggiustaData(Me.txtIncDal.Text) & "&INCAL=" & par.AggiustaData(Me.txtIncAl.Text) & "&VALDAL=" & par.AggiustaData(Me.txtValutaDal.Text) & "&VALAL=" & par.AggiustaData(Me.txtValutaAl.Text) & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "');</script>")
                    End If
                End If
                'Else
                '    Response.Write("<script>alert('Definire almeno la data iniziale di emissione!')</script>")
                '    Exit Sub
            End If
            Response.Write("<script>location.replace('EstrattoConto_New.aspx?ES=" & UCase(ChStornate.Checked) & "&DAL=" & par.AggiustaData(Me.TxtDal.Text) & "&AL=" & par.AggiustaData(Me.TxtAl.Text) & "&RIFDAL=" & par.AggiustaData(Me.TxtRifDal.Text) & "&RIFAL=" & par.AggiustaData(Me.TxtRifAl.Text) & "&INCDAL=" & par.AggiustaData(Me.txtIncDal.Text) & "&INCAL=" & par.AggiustaData(Me.txtIncAl.Text) & "&VALDAL=" & par.AggiustaData(Me.txtValutaDal.Text) & "&VALAL=" & par.AggiustaData(Me.txtValutaAl.Text) & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "');</script>")
        End If
    End Sub


End Class
