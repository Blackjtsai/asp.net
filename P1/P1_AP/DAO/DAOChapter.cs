using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using P1_Core;

namespace P1_AP.DAO;
/// <summary>Chapter存取物件</summary>
public class DAOChapter : DBTBNet6TESTFactory
{
  /// <summary>新增Chapter資料</summary>
  public int insert(chapter chapterObj)
  {
    string strSQL;
    strSQL = @"insert into chapter (chapter_id, chapter_name, chapter_url, status, note , seq) values (@chapter_id, @chapter_name, @chapter_url, @status, @note ,@seq)";
    return Execute(strSQL, chapterObj);
  }

  /// <summary>刪除指定Chapter資料</summary>
  public int delete(chapter chapterObj)
  {
    string strSQL;

    strSQL = @"delete from chapter where chapter_id = @chapter_id";

    return Execute(strSQL, chapterObj);

  }

  /// <summary>更新Chapter資料</summary>
  public int update(chapter chapterObj)
  {
    string strSQL;

    strSQL = @"Update chapter set 
      chapter_name = @chapter_name
      , chapter_url = @chapter_url
      , status = @status
      , note = @note
      ,seq = @seq
      where chapter_id = @chapter_id";

    return Execute(strSQL, chapterObj);

  }

  /// <summary>取Chapter最大ID</summary>
  public dynamic getMaxID()
  {
    string strSQL;
    strSQL = @"select max(chapter_id) as id from chapter ";
    return Query(strSQL, null)[0];
  }

  /// <summary>查詢所有Chapter資料</summary>
  public List<chapter> getAll()
  {
    string strSQL;
    strSQL = @"select * from chapter ";
    return Query<chapter>(strSQL, null);
  }

  /// <summary>依照chapter_id取資料</summary>
  public List<chapter> getDataByID(string chapter_id)
  {
    string strSQL;

    strSQL = @"select * from chapter where chapter_id = @chapter_id";
    return Query<chapter>(strSQL, new { chapter_id = chapter_id });

  }

}