'*** LISTA RISULTATO SEGNALAZIONI

Partial Class RisultatiSegnalazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    ''Dim par As New CM.Global

    Public sValoreStato As String
    Public sOrdinamento As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        If IsPostBack = False Then

            TrovaSegnalazioni()

        End If

    End Sub

    Private Sub TrovaSegnalazioni()
        Dim FlagConnessione As Boolean = False

        Try
            Dim sStr1 As String = ""
            Dim sOrder As String = ""



            Dim sStrID_ID_TIPOLOGIE As String = "-1"
            Dim sID_TIPO_ST As String = ""

            Dim sTipoRichiesta As String = "1" 'TIPO_RICHIESTA = 1  SEGNALAZIONI GUASTI prima era '0=GUASTI',1,'RECLAMI',2,'PROPOSTE',3,'VARIE'



            Dim sFiliale As String = "-1"
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = Session.Item("ID_STRUTTURA")
            End If



            sValoreStato = Request.QueryString("ST")
            sOrdinamento = Request.QueryString("ORD")

            Select Case sOrdinamento
                Case "DATA"
                    sOrder = " order by DATA_INSERIMENTO"
                Case "TIPO"
                    sOrder = " order by SISCOM_MI.SEGNALAZIONI.ID_STATO"
                Case "ID"
                    sOrder = " order by SISCOM_MI.SEGNALAZIONI.ID"
                Case "PER"
                    sOrder = " ORDER BY ID_PERICOLO_SEGNALAZIONE DESC"
                Case Else
                    sOrder = ""
            End Select

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            'sFiliale = 26 X LE PROVE

            If sFiliale > 0 Then

                par.cmd.CommandText = "select * from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    sID_TIPO_ST = par.IfNull(myReader1("ID_TIPO_ST"), 0)
                End If
                myReader1.Close()

                '0=FILIALE AMMINISTRATIVA
                '1=FILIALE TECNICA
                '2=UFFICIO CENTRALE

                par.cmd.CommandText = "select ID from SISCOM_MI.TIPOLOGIE_GUASTI " _
                                  & " where ID_TIPO_ST=" & sID_TIPO_ST

                'If sID_TIPO_ST = 2 Then
                '    '2=UFFICIO CENTRALE vede le segnalazioni di tutti i complessi però con ID_TIPOLOGIE fltrata anche per ID_STRUTTURA
                '    par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale
                'End If
                'myReader1 = par.cmd.ExecuteReader()
                If sID_TIPO_ST = 2 Then
                    '2=UFFICIO CENTRALE vede le segnalazioni di tutti i complessi però con ID_TIPOLOGIE fltrata anche per ID_STRUTTURA
                    'par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale
                End If
                myReader1 = par.cmd.ExecuteReader()

                While myReader1.Read
                    If sStrID_ID_TIPOLOGIE = "" Then
                        sStrID_ID_TIPOLOGIE = myReader1(0)
                    Else
                        sStrID_ID_TIPOLOGIE = sStrID_ID_TIPOLOGIE & "," & myReader1(0)
                    End If
                End While
                myReader1.Close()

                'If sID_TIPO_ST = 1 Then
                '    par.cmd.CommandText = "select ID from SISCOM_MI.TAB_FILIALI where ID_TECNICA=" & sFiliale
                '    myReader1 = par.cmd.ExecuteReader()

                '    While myReader1.Read
                '        If sStrID_TAB_FILIALI = "" Then
                '            sStrID_TAB_FILIALI = myReader1(0)
                '        Else
                '            sStrID_TAB_FILIALI = sStrID_TAB_FILIALI & "," & myReader1(0)
                '        End If
                '    End While
                '    myReader1.Close()
                '    If sStrID_TAB_FILIALI = "" Then sStrID_TAB_FILIALI = "-1"
                'End If

                'If sID_TIPO_ST = 2 And sFiliale = 64 Then
                '    sStrID_ID_TIPOLOGIE = ""
                '    par.cmd.CommandText = "select ID from SISCOM_MI.TIPOLOGIE_GUASTI " _
                '                  & " where ID_TIPO_ST=0"

                '    myReader1 = par.cmd.ExecuteReader()

                '    While myReader1.Read
                '        If sStrID_ID_TIPOLOGIE = "" Then
                '            sStrID_ID_TIPOLOGIE = myReader1(0)
                '        Else
                '            sStrID_ID_TIPOLOGIE = sStrID_ID_TIPOLOGIE & "," & myReader1(0)
                '        End If
                '    End While
                '    myReader1.Close()



                '    sStrID_TAB_FILIALI = "1,6,9"

                'End If

                'If sID_TIPO_ST = 2 And sFiliale = 65 Then
                '    sStrID_ID_TIPOLOGIE = ""
                '    par.cmd.CommandText = "select ID from SISCOM_MI.TIPOLOGIE_GUASTI " _
                '                         & " where ID_TIPO_ST=0"
                '    myReader1 = par.cmd.ExecuteReader()

                '    While myReader1.Read
                '        If sStrID_ID_TIPOLOGIE = "" Then
                '            sStrID_ID_TIPOLOGIE = myReader1(0)
                '        Else
                '            sStrID_ID_TIPOLOGIE = sStrID_ID_TIPOLOGIE & "," & myReader1(0)
                '        End If
                '    End While
                '    myReader1.Close()

                '    sStrID_TAB_FILIALI = "2,8,22"

                'End If


                'COMPLESSO
                sStr1 = sStr1 & " select SEGNALAZIONI.ID,ID_PERICOLO_SEGNALAZIONE AS CRITICITA,ID_PERICOLO_sEGNALAZIONE,(select descrizione from siscom_mi.tipologie_guasti where tipologie_guasti.id = segnalazioni.id_tipologie) as tipo_segnalazione, " _
                            & " ID_COMPLESSO as IDENTIFICATIVO," _
                            & " 'C' as TIPO_S," _
                            & " COGNOME_RS||' '||NOME as RICHIEDENTE," _
                            & " to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO, " _
                            & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC  " _
                     & " from SISCOM_MI.SEGNALAZIONI " _
                     & " where SEGNALAZIONI.ID_STATO=" & sValoreStato _
                     & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
                     & "/*   and SEGNALAZIONI.ID_TIPOLOGIE  in (" & sStrID_ID_TIPOLOGIE & ")*/" _
                     & "   and SEGNALAZIONI.ID_tipo_Segnalazione=1 " _
                     & "   and SEGNALAZIONI.ID_COMPLESSO is NOT NULL " _
                     & "   and SEGNALAZIONI.ID_EDIFICIO is NULL " _
                     & "   and SEGNALAZIONI.ID_UNITA is NULL "

                If sID_TIPO_ST = 0 Then
                    '0=FILIALE AMMINISTRATIVA vede le segnalazioni con i complessi della propria filiale (come era la logica di prima)
                    sStr1 = sStr1 & "   and SEGNALAZIONI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                   & " where ID_FILIALE=" & sFiliale & ") "
                ElseIf sID_TIPO_ST = 1 Then
                    '1=FILIALE TECNICA   vede tutti  complessi della propria filiale più quelle dope TAB_FILALE.ID_TECNICA è uguale alla propria FILIALE
                    'sStr1 = sStr1 & "   and SEGNALAZIONI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI) " _
                    '                                                                     & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ") ) "
                ElseIf sID_TIPO_ST = 2 Then
                    'sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                    '                     & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    '                                                                & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ")) ) "
                End If


                '& "   and SEGNALAZIONI.ID_COMPLESSO is NULL " 
                'EDIFICIO
                sStr1 = sStr1 & " union select SEGNALAZIONI.ID,ID_PERICOLO_SEGNALAZIONE AS CRITICITA,ID_PERICOLO_sEGNALAZIONE,(select descrizione from siscom_mi.tipologie_guasti where tipologie_guasti.id = segnalazioni.id_tipologie) as tipo_segnalazione, " _
                            & " ID_EDIFICIO as IDENTIFICATIVO," _
                            & " 'E' as TIPO_S," _
                            & " COGNOME_RS||' '||NOME as RICHIEDENTE," _
                            & " to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO, " _
                            & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC  " _
                     & " from SISCOM_MI.SEGNALAZIONI " _
                     & " where SISCOM_MI.SEGNALAZIONI.ID_STATO=" & sValoreStato _
                     & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
                     & "/*   and SEGNALAZIONI.ID_TIPOLOGIE  in (" & sStrID_ID_TIPOLOGIE & ")*/" _
                     & "   and SEGNALAZIONI.ID_tipo_Segnalazione=1 " _
                     & "   and SEGNALAZIONI.ID_EDIFICIO is NOT NULL " _
                     & "   and SEGNALAZIONI.ID_UNITA is NULL "

                If sID_TIPO_ST = 0 Then
                    sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                  & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                         & " where ID_FILIALE=" & sFiliale & ") ) "

                ElseIf sID_TIPO_ST = 1 Then
                    'sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                    '                                             & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    '                                                                   & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ")) ) "
                ElseIf sID_TIPO_ST = 2 Then
                    'sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                    '                     & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    '                                          & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ")) ) "
                End If

                'UNITA
                '& "   and SEGNALAZIONI.ID_COMPLESSO Is not NULL " _
                '& "   and SEGNALAZIONI.ID_EDIFICIO Is not NULL " 
                sStr1 = sStr1 & " union select SEGNALAZIONI.ID,ID_PERICOLO_SEGNALAZIONE AS CRITICITA,ID_PERICOLO_sEGNALAZIONE,(select descrizione from siscom_mi.tipologie_guasti where tipologie_guasti.id = segnalazioni.id_tipologie) as tipo_segnalazione, " _
                            & " ID_UNITA as IDENTIFICATIVO," _
                            & " 'U' as TIPO_S," _
                            & " COGNOME_RS||' '||NOME as RICHIEDENTE," _
                            & " to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO, " _
                            & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC  " _
                     & " from SISCOM_MI.SEGNALAZIONI " _
                     & " where SISCOM_MI.SEGNALAZIONI.ID_STATO=" & sValoreStato _
                     & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
                     & "/*   and SEGNALAZIONI.ID_TIPOLOGIE  in (" & sStrID_ID_TIPOLOGIE & ")*/" _
                     & "   and SEGNALAZIONI.ID_tipo_Segnalazione=1 " _
                     & "   and SEGNALAZIONI.ID_UNITA is NOT NULL "


                If sID_TIPO_ST = 0 Then
                    sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_UNITA in (select ID from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                        & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                               & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                     & " where ID_FILIALE=" & sFiliale & ") ))"

                ElseIf sID_TIPO_ST = 1 Then
                    'sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_UNITA in (select ID from SISCOM_MI.UNITA_IMMOBILIARI " _
                    '                                   & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                    '                                         & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    '                                               & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ")) ))"
                ElseIf sID_TIPO_ST = 2 Then
                    'sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                    '                     & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    '                                          & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ")) ) "
                End If



            Else

                sStr1 = "select SEGNALAZIONI.ID,ID_PERICOLO_SEGNALAZIONE AS CRITICITA,ID_PERICOLO_sEGNALAZIONE,(select descrizione from siscom_mi.tipologie_guasti where tipologie_guasti.id = segnalazioni.id_tipologie) as tipo_segnalazione," _
                            & " case when ID_UNITA      is not null then ID_UNITA " _
                                  & "when ID_EDIFICIO   is not null then ID_EDIFICIO " _
                                  & "when ID_COMPLESSO  is not null then ID_COMPLESSO " _
                            & " end as IDENTIFICATIVO," _
                            & " case when ID_UNITA      is not null then 'U' " _
                                 & " when ID_EDIFICIO   is not null then 'E' " _
                                 & " when ID_COMPLESSO  is not null then 'C' " _
                            & " end as TIPO_S,COGNOME_RS||' '||NOME as RICHIEDENTE," _
                            & "to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO," _
                            & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC " _
                        & " from SISCOM_MI.SEGNALAZIONI " _
                        & " where SISCOM_MI.SEGNALAZIONI.ID_STATO=" & sValoreStato _
                        & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
                        & "   and SEGNALAZIONI.ID_tipo_Segnalazione=1 "


            End If

            sStr1 = sStr1 & sOrder

            '*** CARICO LA GRIGLIA
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStr1, par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "SISCOM_MI.SEGNALAZIONI")

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            Label1.Text = " " & ds.Tables(0).Rows.Count


            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""../../Pagina_home.aspx""</script>")
    End Sub


    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            Session.Add("ID", txtid.Text)

            sValoreStato = Request.QueryString("ST")
            sOrdinamento = Request.QueryString("ORD")

            Response.Write("<script>location.replace('Segnalazioni.aspx?IDS=" & txtid.Text & "&ST=" & sValoreStato & "&ORD=" & sOrdinamento & "');</script>")


        End If

    End Sub





    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la segnalazione N°: " & Replace(e.Item.Cells(0).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

            ''            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l'impianto del Cod. Complesso: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la segnalazione N°: " & Replace(e.Item.Cells(0).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

            ''            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l'impianto del Cod. Complesso: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
            ';document.getElementById('txtImpianto').value='" & e.Item.Cells(2).Text & "'"
        End If

        Select Case e.Item.Cells(par.IndDGC(DataGrid1, "CRITICITA")).Text
            Case "0"
                e.Item.Cells(par.IndDGC(DataGrid1, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                    & "<tr><td><img src=""Immagini/Ball-white-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                    & "</<tr></table>"
            Case "1"
                e.Item.Cells(par.IndDGC(DataGrid1, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                    & "<tr><td><img src=""Immagini/Ball-green-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                    & "</<tr></table>"
            Case "2"
                e.Item.Cells(par.IndDGC(DataGrid1, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                    & "<tr><td><img src=""Immagini/Ball-yellow-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                    & "</<tr></table>"
            Case "3"
                e.Item.Cells(par.IndDGC(DataGrid1, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                    & "<tr><td><img src=""Immagini/Ball-red-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                    & "</<tr></table>"
            Case "4"
                e.Item.Cells(par.IndDGC(DataGrid1, "CRITICITA")).Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                    & "<tr><td><img src=""Immagini/Ball-blue-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                    & "</<tr></table>"
            Case Else
        End Select


    End Sub


    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Dim scriptblock As String

        'BindExcel()
        'Exit Sub

        Try

            sValoreStato = Request.QueryString("ST")
            sOrdinamento = Request.QueryString("ORD")


            'Response.Write("<script>window.open('Report/ReportRisultatoImpianti.aspx?IMP1=1,&Pas='" & Session.Item("IMP2") & "');</script>")
            'Response.Write("<script>location.replace('RisultatiImpianti.aspx?CO=" & sValoreComplesso & "&ED=" & sValoreEdificio & "&IM=" & sValoreImpianto & "');</script>")

            'btnStampa.Attributes.Add("OnClick", "javascript:window.open('Report/ReportRisultatoSegnalazioni.aspx?ST=" & sValoreStato & "&ORD=" & sOrdinamento & "');")

            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "window.open('Report/ReportRisultatoSegnalazioni.aspx?ST=" & sValoreStato & "&ORD=" & sOrdinamento & "','Report');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub



End Class
