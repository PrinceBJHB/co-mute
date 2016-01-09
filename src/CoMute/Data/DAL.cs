using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Data
{
	public static class DAL
	{
		static Entities entities;

		public static Entities DataEntities
		{
			get
			{
				if (entities == null) entities = new Entities();
				return DAL.entities;
			}
			set { DAL.entities = value; }
		}
	}
}

/*
MS SQL Server address:	co-mute.mssql.somee.com
Login name:	EclipseZA_SQLLogin_1
Login password:	4bj6rjwd8c
Connection string:	workstation id=co-mute.mssql.somee.com;packet size=4096;user id=EclipseZA_SQLLogin_1;pwd=4bj6rjwd8c;data source=co-mute.mssql.somee.com;persist security info=False;initial catalog=co-mute
 */
