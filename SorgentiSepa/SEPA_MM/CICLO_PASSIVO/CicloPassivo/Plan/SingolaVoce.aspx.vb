
Partial Class Contabilita_CicloPassivo_Plan_SingolaVoce
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            idPianoF.Value = Request.QueryString("IDP")
            idVoce.Value = Request.QueryString("IDV")

            CARICAIVA()
            CaricaStato()
            CaricaVoce()

            CaricaAlert()

        End If

        Dim CTRL As Control
        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('modificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('modificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('modificato').value='1';")
            End If
        Next

        txtImporto.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        txtImporto.Attributes.Add("onchange", "javascript:document.getElementById('lblLordo').innerHTML='0,00';document.getElementById('cmbIva').value='0';")

        cmbIva.Attributes.Add("onchange", "javascript:document.getElementById('lblLordo').innerHTML= (parseFloat(document.getElementById('txtImporto').value.replace(',', '.')) + (parseFloat(document.getElementById('txtImporto').value.replace(',', '.'))*parseFloat(document.getElementById('cmbIva').value))/100).toFixed(2).replace('.',',');")
        lblLordo.Text = Format(txtImporto.Text + ((txtImporto.Text * cmbIva.SelectedItem.Value) / 100), "0.00")
    End Sub

    Function CaricaAlert()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select * from siscom_mi.pf_voci_STRUTTURA where id_VOCE=" & idVoce.Value & " and id_struttura=" & Session.Item("ID_STRUTTURA")
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                If par.IfNull(myReader5("completo_aler"), "0") = "1" And par.IfNull(myReader5("completo"), "0") = "0" And par.IfNull(myReader5("valore_lordo_aler"), "0") <> par.IfNull(myReader5("valore_lordo"), "0") Then
                    ImgAvviso.Visible = True
                    lblAvviso.Visible = True
                    lblAvviso.Text = "ATTENZIONE...<br/>Questa voce del Piano Finanziario non è stata approvata dal Gestore! L'importo lordo deve essere pari a euro : " & Format(CDbl(par.IfNull(myReader5("valore_lordo_aler"), "0")), "##,##0.00")
                    suggerito.Value = par.IfNull(myReader5("valore_lordo_aler"), "0")
                End If
            End If
            myReader5.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Function CaricaVoce()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select pf_voci_STRUTTURA.*,PF_VOCI.CODICE,PF_VOCI.DESCRIZIONE,PF_VOCI.INDICE from SISCOM_MI.PF_VOCI, siscom_mi.pf_voci_STRUTTURA where PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID AND id_VOCE=" & idVoce.Value & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                lblVoce.Text = UCase(par.IfNull(myReader5("codice"), "") & "-" & par.IfNull(myReader5("descrizione"), ""))

                If myReader5("INDICE") >= 22 And myReader5("INDICE") <= 40 Then
                    'par.cmd.CommandText = "select sum(valore_canone*(1+(iva_canone/100))) from siscom_mi.pf_voci_importo where id_voce=" & idVoce.Value
                    'Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReader6.Read Then
                    '    lblImporto.Text = Format(par.IfNull(myReader6(0), "0"), "0.00")
                    'End If
                    'myReader6.Close()

                Else
                    txtImporto.Text = Format(par.IfNull(myReader5("valore_netto"), "0"), "0.00")
                    cmbIva.ClearSelection()
                    cmbIva.Items.FindByValue(par.IfNull(myReader5("iva"), "0")).Selected = True

                    lblLordo.Visible = True
                    lblLordo.Text = Format(par.IfNull(myReader5("valore_lordo"), "0"), "0.00") 'Format(txtImporto.Text + ((txtImporto.Text * cmbIva.SelectedItem.Value) / 100), "0.00")


                    ImgServizio.Visible = False
                    'lblImporto.Visible = False
                    txtImporto.Visible = True
                End If

                If par.IfNull(myReader5("completo"), "0") = "1" Then
                    ChCompleto.Checked = True
                Else
                    ChCompleto.Checked = False
                End If
            End If
            myReader5.Close()

            par.cmd.Dispose()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function


    Function CaricaStato()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
            End If
            myReader5.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function



    Protected Sub ImgServizio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgServizio.Click
        'CaricaImporto()
    End Sub

    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            Dim importo As String = 0

            If txtimporto.visible = False Then
                'importo = lblImporto.Text
            Else
                importo = txtimporto.text
            End If

            'If importo > 0 Then
            If ChCompleto.Checked = True Then

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                    & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                   & "'F82','" & par.PulisciStrSql(lblVoce.Text) & "'," & Session.Item("ID_STRUTTURA") & ")"
                par.cmd.ExecuteNonQuery()


                par.cmd.CommandText = "update siscom_mi.pf_voci_struttura set completo='1',valore_lordo=" & par.VirgoleInPunti(Format((importo + (importo * cmbIva.SelectedItem.Value) / 100), "0.00")) & ",valore_netto=" & par.VirgoleInPunti(importo) & ",iva=" & cmbIva.SelectedItem.Value & " where id_voce=" & idVoce.Value & " and id_struttura=" & Session.Item("ID_STRUTTURA")

            Else

                par.cmd.CommandText = "update siscom_mi.pf_voci_struttura set completo='0',valore_lordo=" & par.VirgoleInPunti(Format((importo + (importo * cmbIva.SelectedItem.Value) / 100), "0.00")) & ",valore_netto=" & par.VirgoleInPunti(importo) & ",iva=" & cmbIva.SelectedItem.Value & " where id_voce=" & idVoce.Value & " and id_struttura=" & Session.Item("ID_STRUTTURA")


            End If
            par.cmd.ExecuteNonQuery()


            par.myTrans.Commit()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Operazione Effettuata!');</script>")
            modificato.Value = "0"

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub ChCompleto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChCompleto.CheckedChanged
        If txtImporto.Visible = False Then
            If VerificaCompleta(idVoce.Value) = False Then
                Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme divise non coincidono!');</script>")
                ChCompleto.Checked = False
            End If
        End If

        If VerificaAler(idVoce.Value) = False Then
            Response.Write("<script>alert('Attenzione...La voce non può essere definita completa perchè le somme non coincidono con quanto imposto dal Gestore!');</script>")
            ChCompleto.Checked = False
        End If
    End Sub

    Private Function VerificaCompleta(ByVal idVoce As String) As Boolean
        Try
            VerificaCompleta = True

            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim Importo As Double = 0
            Dim Importo1 As Double = 0
            Dim testo As String = ""

            par.cmd.CommandText = "select * from siscom_mi.pf_voci_importo where id_voce=" & idVoce
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader5.Read
                Importo = par.IfNull(myReader5("valore"), 0) * (1 + (par.IfNull(myReader5("iva"), 0) / 100))

                par.cmd.CommandText = "select sum(importo_CANONE_LORDO+IMPORTO_CONSUMO_LORDO) from siscom_mi.lotti_patrimonio_importi where id_voce_importo=" & myReader5("id")
                Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader6.Read Then
                    Importo1 = par.IfNull(myReader6(0), 0)
                End If
                myReader6.Close()

                If Importo <> Importo1 Then
                    VerificaCompleta = False

                End If
                Importo = 0
                Importo1 = 0
            Loop

            myReader5.Close()



            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try



    End Function

    Private Function VerificaAler(ByVal idVoce As String) As Boolean
        Try
            VerificaAler = True

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim Importo As Double = 0

            par.cmd.CommandText = "select * from siscom_mi.pf_voci_STRUTTURA where id_VOCE=" & idVoce & " and id_struttura=" & Session.Item("ID_STRUTTURA")
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Importo = Format(txtImporto.Text * (1 + (cmbIva.SelectedItem.Value / 100)), "0.00")
                If par.IfNull(myReader5("completo_aler"), "0") = "1" And par.IfNull(myReader5("completo"), "0") = "0" And par.IfNull(myReader5("valore_lordo_aler"), "0") <> Importo Then
                    VerificaAler = False
                End If
            End If
            myReader5.Close()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try



    End Function

    Private Sub CaricaIva()
        Try
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC", cmbIva, "VALORE", "VALORE", False)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

End Class
