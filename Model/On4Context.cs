using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WEBAPI_Bravo.Model
{
    public partial class on4Context : DbContext
    {
        public on4Context()
        {
        }

        public on4Context(DbContextOptions<on4Context> options)
            : base(options)
        {
        }
        public DbSet<TransTicket> TransTickets { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=10.190.30.69;Database=oct_pertamina;User=root;Password=P3rt4m!n@Pcc135Pr0d;");
            }
        }

       
       



       

       
       
       

       

       

       

       
       

       

       
       
       

       
       
       
       

       
       
       
       

       
       
       
       

       
       
       
       

       
       
       
       

       
       
       
       

       
       
       
       

       
       
       
       

       
       
       
       

       
       
       

       
       
       

       
       
       

       
       
       
       
       

       
       
       
       

       
       
       
       

       
       
       
       
       

            modelBuilder.Entity<InteractionFacebookMessage>(entity =>
            {
                entity.ToTable("interaction_facebook_message");

                entity.HasIndex(e => e.CustId, "FK_08e3245092f020d2ae5f646cfd0");

                entity.HasIndex(e => e.CreatedBy, "FK_ccd1e1ecf4fbb420ed3447f7ee5");

                entity.HasIndex(e => e.ActionType, "IDX_13f259a7939644cc61d63766d8");

                entity.HasIndex(e => e.DateReceived, "IDX_4e431b8edceebbd210c37ead62");

                entity.HasIndex(e => e.SessionId, "IDX_6a24aad801d5c8679aa51b6dba");

                entity.HasIndex(e => e.DateMiddleware, "IDX_6bc86530e59c6bfdf233847e4c");

                entity.HasIndex(e => e.MessageType, "IDX_b8554f49a5a5b587a43dc31475");

                entity.HasIndex(e => e.StreamId, "IDX_c54303e0c724dc85066b286b83")
                    .IsUnique();

                entity.HasIndex(e => e.DateOrigin, "IDX_df3cd64885615e5f3e65272946");

                entity.HasIndex(e => e.DateRead, "IDX_eb82dd64a12dcbd20fccdbcdb6");

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.ActionType)
                    .IsRequired()
                    .HasColumnType("enum('IN','OUT','FOLLOWUP','MEMO','REPLYBACK')")
                    .HasColumnName("action_type")
                    .HasDefaultValueSql("'''IN'''");

                entity.Property(e => e.ConvId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("conv_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.DateMiddleware)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_middleware")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateOrigin)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_origin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateRead)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_read")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateReceived)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_received")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.Media)
                    .HasColumnName("media")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MediaOriginal)
                    .HasColumnName("media_original")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MessageType)
                    .IsRequired()
                    .HasColumnType("enum('sticker','gif','video','image','file','location','audio','text')")
                    .HasColumnName("message_type")
                    .HasDefaultValueSql("'''text'''");

                entity.Property(e => e.QuoteMessage)
                    .HasColumnName("quote_message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ResponseTime)
                    .HasColumnType("int(11)")
                    .HasColumnName("response_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SendDetail)
                    .HasColumnName("send_detail")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SendStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("send_status")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("session_id");

                entity.Property(e => e.StreamId)
                    .HasMaxLength(200)
                    .HasColumnName("stream_id")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InteractionFacebookMessages)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("interaction_facebook_message_ibfk_2");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.InteractionFacebookMessages)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_facebook_message_ibfk_1");
            });

            modelBuilder.Entity<InteractionFacebookMessageHistory>(entity =>
            {
                entity.ToTable("interaction_facebook_message_history");

                entity.HasIndex(e => e.CustId, "FK_580eefd5be35a85d7034ab74931");

                entity.HasIndex(e => e.CreatedBy, "FK_58200a0b703d46ab2573df6bcdf");

                entity.HasIndex(e => e.SessionId, "IDX_0d35fb8e030b773f7de2dedf65");

                entity.HasIndex(e => e.DateReceived, "IDX_145cb34790a23321ad355fc120");

                entity.HasIndex(e => e.MessageType, "IDX_3216496928f015c568bd6bc1a0");

                entity.HasIndex(e => e.DateOrigin, "IDX_591afbfbc920802857f9fe823f");

                entity.HasIndex(e => e.DateMiddleware, "IDX_639eeed41511a74878715be86e");

                entity.HasIndex(e => e.StreamId, "IDX_925635e5b433352b6a136466b4")
                    .IsUnique();

                entity.HasIndex(e => e.DateRead, "IDX_cc8c6efa7b7c1281640dbfe7e3");

                entity.HasIndex(e => e.ActionType, "IDX_d14989b49e4793b0a323c4cf0d");

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.ActionType)
                    .IsRequired()
                    .HasColumnType("enum('IN','OUT','FOLLOWUP','MEMO','REPLYBACK')")
                    .HasColumnName("action_type")
                    .HasDefaultValueSql("'''IN'''");

                entity.Property(e => e.ConvId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("conv_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.DateMiddleware)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_middleware")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateOrigin)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_origin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateRead)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_read")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateReceived)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_received")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.Media)
                    .HasColumnName("media")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MediaOriginal)
                    .HasColumnName("media_original")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MessageType)
                    .IsRequired()
                    .HasColumnType("enum('sticker','gif','video','image','file','location','audio','text')")
                    .HasColumnName("message_type")
                    .HasDefaultValueSql("'''text'''");

                entity.Property(e => e.QuoteMessage)
                    .HasColumnName("quote_message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ResponseTime)
                    .HasColumnType("int(11)")
                    .HasColumnName("response_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SendDetail)
                    .HasColumnName("send_detail")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SendStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("send_status")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("session_id");

                entity.Property(e => e.StreamId)
                    .HasMaxLength(200)
                    .HasColumnName("stream_id")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InteractionFacebookMessageHistories)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("interaction_facebook_message_history_ibfk_2");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.InteractionFacebookMessageHistories)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_facebook_message_history_ibfk_1");
            });

            modelBuilder.Entity<InteractionHeader>(entity =>
            {
                entity.ToTable("interaction_header");

                entity.HasIndex(e => e.GroupId, "FK_5c2c56aa907f7c9af6482b0753f");

                entity.HasIndex(e => e.ChannelId, "FK_cef581f6b0bca545920009f5314");

                entity.HasIndex(e => e.AgentId, "FK_dd5f4cf0a26e2604ddfdc9e0279");

                entity.HasIndex(e => e.CustId, "FK_fb1cae866534ebe0b85cec12651");

                entity.HasIndex(e => e.UniqueId, "IDX_5f3df22ec91161e81201a79cdc")
                    .IsUnique();

                entity.HasIndex(e => e.DateFirstPickup, "IDX_5f9bd7e01872e83618e9b2572e");

                entity.HasIndex(e => e.DateEnd, "IDX_601e45a84f8e52fe0dcfc7ab9e");

                entity.HasIndex(e => e.DateStart, "IDX_86c8b3d43faa7b6cd18979318f");

                entity.HasIndex(e => e.DatePickup, "IDX_9f495562b1066d6394dd2857b5");

                entity.HasIndex(e => e.IsRtc, "IDX_ad634264dad055795c42be2999");

                entity.HasIndex(e => e.FromId, "IDX_af338ff6d19c51fb73c4703ebe");

                entity.HasIndex(e => e.ConvId, "IDX_c32ec125ca4a8813e9ffe89810");

                entity.HasIndex(e => e.SessionId, "IDX_c5f7e0ee1dfe0323c35db00840")
                    .IsUnique();

                entity.HasIndex(e => e.DateFirstResponse, "IDX_d035f2efac47c19041054ceacf");

                entity.HasIndex(e => e.DateOrigin, "IDX_d43c83591d40ecb074b11de9fe");

                entity.HasIndex(e => e.Account, "IDX_e69abbb391bacbe3b549abedda");

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(100)
                    .HasColumnName("account_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AdditionalInfo)
                    .HasColumnName("additional_info")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AgentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("agent_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ChannelId)
                    .HasColumnType("int(11)")
                    .HasColumnName("channel_id");

                entity.Property(e => e.ConvId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("conv_id");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_end")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateFirstPickup)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_first_pickup")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateFirstResponse)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_first_response")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateOrigin)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_origin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DatePickup)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_pickup")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateStart)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_start");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("group_id");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsAbandon)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_abandon");

                entity.Property(e => e.IsCreateCase)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_createCase");

                entity.Property(e => e.IsRtc)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_rtc")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastMessage)
                    .HasColumnName("last_message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Priority)
                    .HasColumnType("int(11)")
                    .HasColumnName("priority");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("session_id");

                entity.Property(e => e.Subject)
                    .HasColumnName("subject")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UniqueId)
                    .HasMaxLength(200)
                    .HasColumnName("unique_id")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.InteractionHeaders)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("interaction_header_ibfk_3");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.InteractionHeaders)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_header_ibfk_2");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.InteractionHeaders)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_header_ibfk_4");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.InteractionHeaders)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_header_ibfk_1");
            });

            modelBuilder.Entity<InteractionHeaderHistory>(entity =>
            {
                entity.ToTable("interaction_header_history");

                entity.HasIndex(e => e.CustId, "FK_a382946706f3b3aa29f1ebac2b2");

                entity.HasIndex(e => e.AgentId, "FK_c930a2c7860626c1e2a394e796e");

                entity.HasIndex(e => e.ChannelId, "FK_cdcb725b75e0ebd68eda20e17a8");

                entity.HasIndex(e => e.GroupId, "FK_f6bf6ccf18a77679373fb206566");

                entity.HasIndex(e => e.IsRtc, "IDX_0d8ecd162729983e9eee40f01b");

                entity.HasIndex(e => e.DateEnd, "IDX_256dba035589804fffdf7af5a2");

                entity.HasIndex(e => e.DateOrigin, "IDX_267f015ff9132490a6ef16d6a9");

                entity.HasIndex(e => e.UniqueId, "IDX_27001cf472e06b03a47edb6f42")
                    .IsUnique();

                entity.HasIndex(e => e.SessionId, "IDX_49edf0ce815b4996561485addf")
                    .IsUnique();

                entity.HasIndex(e => e.Account, "IDX_4dfd066d050d93a26f08cb5a75");

                entity.HasIndex(e => e.FromId, "IDX_595cc400a7ab0529c48f3d42bb");

                entity.HasIndex(e => e.DateFirstResponse, "IDX_5f85e7a722549e3613ba03d6c6");

                entity.HasIndex(e => e.ConvId, "IDX_ad9c79f05194063099bb9ace63");

                entity.HasIndex(e => e.DateFirstPickup, "IDX_b2911161d8ee13f93788e1b130");

                entity.HasIndex(e => e.DatePickup, "IDX_bf978ee93dee0982dbef665ad3");

                entity.HasIndex(e => e.DateStart, "IDX_e18b8426fee8c875dfb156dcc6");

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .HasColumnName("account")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(100)
                    .HasColumnName("account_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AdditionalInfo)
                    .HasColumnName("additional_info")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AgentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("agent_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ChannelId)
                    .HasColumnType("int(11)")
                    .HasColumnName("channel_id");

                entity.Property(e => e.ConvId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("conv_id");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_end")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateFirstPickup)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_first_pickup")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateFirstResponse)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_first_response")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateOrigin)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_origin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DatePickup)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_pickup")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateStart)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_start");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("group_id");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsAbandon)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_abandon");

                entity.Property(e => e.IsCreateCase)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_createCase");

                entity.Property(e => e.IsRtc)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_rtc")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastMessage)
                    .HasColumnName("last_message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Priority)
                    .HasColumnType("int(11)")
                    .HasColumnName("priority");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("session_id");

                entity.Property(e => e.Subject)
                    .HasColumnName("subject")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UniqueId)
                    .HasMaxLength(200)
                    .HasColumnName("unique_id")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.InteractionHeaderHistories)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("interaction_header_history_ibfk_2");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.InteractionHeaderHistories)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_header_history_ibfk_3");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.InteractionHeaderHistories)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_header_history_ibfk_1");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.InteractionHeaderHistories)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_header_history_ibfk_4");
            });

            modelBuilder.Entity<InteractionHeaderTakeout>(entity =>
            {
                entity.ToTable("interaction_header_takeout");

                entity.HasIndex(e => e.ChannelId, "FK_3e454db014aa077be08b59dae1c");

                entity.HasIndex(e => e.AgentId, "FK_559f7568e8d06c07a2a3f1f9add");

                entity.HasIndex(e => e.GroupId, "FK_be119a53fb4420bf36bf5bed369");

                entity.HasIndex(e => e.CustId, "FK_ce3937c2d1b2dc23d95fd5bd53f");

                entity.HasIndex(e => e.SessionId, "IDX_1a676253375bdae05772f7780d")
                    .IsUnique();

                entity.HasIndex(e => e.DateFirstResponse, "IDX_338d88aa30d70b5957e4b058d6");

                entity.HasIndex(e => e.Account, "IDX_54ec63f9945c27449831d40e3a");

                entity.HasIndex(e => e.UniqueId, "IDX_5922a3a6fef3ba2aab78232c8b")
                    .IsUnique();

                entity.HasIndex(e => e.DatePickup, "IDX_6d5bd0cc03f331c5dc7acab715");

                entity.HasIndex(e => e.DateEnd, "IDX_7fe13cd1a058fb40153fd84b8b");

                entity.HasIndex(e => e.DateStart, "IDX_bce653a2ed9681692871ff4a68");

                entity.HasIndex(e => e.IsRtc, "IDX_bde1aa736a427f6f58eaac7b38");

                entity.HasIndex(e => e.FromId, "IDX_d3537dc02fa69bb4c89bc3a6cd");

                entity.HasIndex(e => e.DateOrigin, "IDX_d6d850706a1e9d2ccb70eb2a20");

                entity.HasIndex(e => e.ConvId, "IDX_e0ccfa7a3dee3d42fc42d86139");

                entity.HasIndex(e => e.DateFirstPickup, "IDX_ef6412984c940264fe4eb8e0ba");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(100)
                    .HasColumnName("account_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AdditionalInfo)
                    .HasColumnName("additional_info")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AgentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("agent_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ChannelId)
                    .HasColumnType("int(11)")
                    .HasColumnName("channel_id");

                entity.Property(e => e.ConvId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("conv_id");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_end")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateFirstPickup)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_first_pickup")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateFirstResponse)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_first_response")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateOrigin)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_origin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DatePickup)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_pickup")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateStart)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_start");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.GroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("group_id");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsAbandon)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_abandon");

                entity.Property(e => e.IsCreateCase)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_createCase");

                entity.Property(e => e.IsRtc)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_rtc")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.LastMessage)
                    .HasColumnName("last_message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Priority)
                    .HasColumnType("int(11)")
                    .HasColumnName("priority");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("session_id");

                entity.Property(e => e.Subject)
                    .HasColumnName("subject")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UniqueId)
                    .HasMaxLength(200)
                    .HasColumnName("unique_id")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.InteractionHeaderTakeouts)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("interaction_header_takeout_ibfk_2");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.InteractionHeaderTakeouts)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_header_takeout_ibfk_1");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.InteractionHeaderTakeouts)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_header_takeout_ibfk_4");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.InteractionHeaderTakeouts)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_header_takeout_ibfk_3");
            });

            modelBuilder.Entity<InteractionInstagramComment>(entity =>
            {
                entity.ToTable("interaction_instagram_comment");

                entity.HasIndex(e => e.CreatedBy, "FK_04860e4eb0578888e5be8f384d5");

                entity.HasIndex(e => e.CustId, "FK_962a1632f43fa6b816afb3760b4");

                entity.HasIndex(e => e.SessionId, "IDX_0b1be76e075a9e15d42002d561");

                entity.HasIndex(e => e.DateReceived, "IDX_0d11cc2ba45dbf9d2f9b37ddd1");

                entity.HasIndex(e => e.StreamId, "IDX_171ca18021d78f90baa98413e1")
                    .IsUnique();

                entity.HasIndex(e => e.DateOrigin, "IDX_67a3bac120a9478aa399cb5981");

                entity.HasIndex(e => e.DateMiddleware, "IDX_82c7855cf920bebcf01a566d40");

                entity.HasIndex(e => e.MessageType, "IDX_d71bec4a2b3d08eb090d1cba56");

                entity.HasIndex(e => e.ActionType, "IDX_ea5c8c6d52a73ccb26a70b67b6");

                entity.HasIndex(e => e.DateRead, "IDX_eeb606d39309e03bdc0132fe4f");

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.ActionType)
                    .IsRequired()
                    .HasColumnType("enum('IN','OUT','FOLLOWUP','MEMO','REPLYBACK')")
                    .HasColumnName("action_type")
                    .HasDefaultValueSql("'''IN'''");

                entity.Property(e => e.CommentType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("comment_type");

                entity.Property(e => e.ConvId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("conv_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.DateMiddleware)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_middleware")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateOrigin)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_origin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateRead)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_read")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateReceived)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_received")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.Media)
                    .HasColumnName("media")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MediaOriginal)
                    .HasColumnName("media_original")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MessageType)
                    .IsRequired()
                    .HasColumnType("enum('sticker','gif','video','image','file','location','audio','text')")
                    .HasColumnName("message_type")
                    .HasDefaultValueSql("'''text'''");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(200)
                    .HasColumnName("parent_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.QuoteMessage)
                    .HasColumnName("quote_message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ResponseTime)
                    .HasColumnType("int(11)")
                    .HasColumnName("response_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SendDetail)
                    .HasColumnName("send_detail")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SendStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("send_status")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("session_id");

                entity.Property(e => e.StreamId)
                    .HasMaxLength(200)
                    .HasColumnName("stream_id")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InteractionInstagramComments)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("interaction_instagram_comment_ibfk_1");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.InteractionInstagramComments)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_instagram_comment_ibfk_2");
            });

            modelBuilder.Entity<InteractionInstagramCommentTraffic>(entity =>
            {
                entity.ToTable("interaction_instagram_comment_traffic");

                entity.HasIndex(e => e.StreamId, "IDX_c3032efaf2d728c7d88d566232")
                    .IsUnique();

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.CommentType)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("comment_type");

                entity.Property(e => e.DateMiddleware)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_middleware")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateOrigin)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_origin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateReceived)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_received")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Media)
                    .HasColumnName("media")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MediaOriginal)
                    .HasColumnName("media_original")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MediaPermalink)
                    .HasMaxLength(100)
                    .HasColumnName("media_permalink")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MessageType)
                    .IsRequired()
                    .HasColumnType("enum('sticker','gif','video','image','file','location','audio','text')")
                    .HasColumnName("message_type")
                    .HasDefaultValueSql("'''text'''");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(200)
                    .HasColumnName("parent_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.StreamId)
                    .HasMaxLength(200)
                    .HasColumnName("stream_id")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<InteractionInstagramMessage>(entity =>
            {
                entity.ToTable("interaction_instagram_message");

                entity.HasIndex(e => e.CreatedBy, "FK_ee10f41f6b5aade731bdb74d7e7");

                entity.HasIndex(e => e.CustId, "FK_f7a88eff19ae240f4df14ba8f36");

                entity.HasIndex(e => e.MessageType, "IDX_0bfe7fae3cb25c999050424b1a");

                entity.HasIndex(e => e.StreamId, "IDX_118a715bd3e490b3eb3878d11f")
                    .IsUnique();

                entity.HasIndex(e => e.DateMiddleware, "IDX_486e1f8fe36348f9450fa9e4cb");

                entity.HasIndex(e => e.ActionType, "IDX_4f7c3e4557d48dbe2c5b7e83b1");

                entity.HasIndex(e => e.DateRead, "IDX_533041e495d5ac409148ac21bf");

                entity.HasIndex(e => e.SessionId, "IDX_c2b27fa53ec3aacb76c081dc0d");

                entity.HasIndex(e => e.DateReceived, "IDX_e7177afdb996b16ffe358cfa03");

                entity.HasIndex(e => e.DateOrigin, "IDX_faf0daf2faa31a59715ab81fd5");

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.ActionType)
                    .IsRequired()
                    .HasColumnType("enum('IN','OUT','FOLLOWUP','MEMO','REPLYBACK')")
                    .HasColumnName("action_type")
                    .HasDefaultValueSql("'''IN'''");

                entity.Property(e => e.ConvId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("conv_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.DateMiddleware)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_middleware")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateOrigin)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_origin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateRead)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_read")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateReceived)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_received")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.Media)
                    .HasColumnName("media")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MediaOriginal)
                    .HasColumnName("media_original")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MessageType)
                    .IsRequired()
                    .HasColumnType("enum('sticker','gif','video','image','file','location','audio','text')")
                    .HasColumnName("message_type")
                    .HasDefaultValueSql("'''text'''");

                entity.Property(e => e.QuoteMessage)
                    .HasColumnName("quote_message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ResponseTime)
                    .HasColumnType("int(11)")
                    .HasColumnName("response_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SendDetail)
                    .HasColumnName("send_detail")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SendStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("send_status")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("session_id");

                entity.Property(e => e.StreamId)
                    .HasMaxLength(200)
                    .HasColumnName("stream_id")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InteractionInstagramMessages)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("interaction_instagram_message_ibfk_1");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.InteractionInstagramMessages)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_instagram_message_ibfk_2");
            });

            modelBuilder.Entity<InteractionInstagramMessageHistory>(entity =>
            {
                entity.ToTable("interaction_instagram_message_history");

                entity.HasIndex(e => e.CustId, "FK_a7b9f690a8e24b9798cde9ad804");

                entity.HasIndex(e => e.CreatedBy, "FK_daf4dbf4736853178beb16e53f4");

                entity.HasIndex(e => e.StreamId, "IDX_1a5defaca64b9858e620d07247")
                    .IsUnique();

                entity.HasIndex(e => e.DateReceived, "IDX_7886bd600048505f37d4a563ab");

                entity.HasIndex(e => e.DateRead, "IDX_9987604d09a4e14c147918b272");

                entity.HasIndex(e => e.DateMiddleware, "IDX_b8d1a75bb13bcf2357f299b9a1");

                entity.HasIndex(e => e.SessionId, "IDX_d3e465176fe7bf3a7940757dc5");

                entity.HasIndex(e => e.DateOrigin, "IDX_d650efcd6343de6e914b4445d2");

                entity.HasIndex(e => e.MessageType, "IDX_f131af49dc620a1e3d3c5898ca");

                entity.HasIndex(e => e.ActionType, "IDX_f9da610730be154e73651eaf46");

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.ActionType)
                    .IsRequired()
                    .HasColumnType("enum('IN','OUT','FOLLOWUP','MEMO','REPLYBACK')")
                    .HasColumnName("action_type")
                    .HasDefaultValueSql("'''IN'''");

                entity.Property(e => e.ConvId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("conv_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.DateMiddleware)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_middleware")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateOrigin)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_origin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateRead)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_read")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateReceived)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_received")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.Media)
                    .HasColumnName("media")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MediaOriginal)
                    .HasColumnName("media_original")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MessageType)
                    .IsRequired()
                    .HasColumnType("enum('sticker','gif','video','image','file','location','audio','text')")
                    .HasColumnName("message_type")
                    .HasDefaultValueSql("'''text'''");

                entity.Property(e => e.QuoteMessage)
                    .HasColumnName("quote_message")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ResponseTime)
                    .HasColumnType("int(11)")
                    .HasColumnName("response_time")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SendDetail)
                    .HasColumnName("send_detail")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SendStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("send_status")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("session_id");

                entity.Property(e => e.StreamId)
                    .HasMaxLength(200)
                    .HasColumnName("stream_id")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InteractionInstagramMessageHistories)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("interaction_instagram_message_history_ibfk_2");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.InteractionInstagramMessageHistories)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_instagram_message_history_ibfk_1");
            });

            modelBuilder.Entity<InteractionManual>(entity =>
            {
                entity.ToTable("interaction_manual");

                entity.HasIndex(e => e.UpdatedBy, "FK_b1b284a2882285bdee743d91bf0");

                entity.HasIndex(e => e.SourceId, "FK_cc3de311635bd7491d91ea6a7b9");

                entity.HasIndex(e => e.CreatedBy, "FK_efa7af8906f49008be7a03b0780");

                entity.HasIndex(e => e.CustId, "FK_f171a5c7a18967886062c2e48e4");

                entity.HasIndex(e => e.DateCreate, "IDX_32df5bedff630485eb8a02c76d");

                entity.HasIndex(e => e.SessionId, "IDX_bd2359191259184a0df53739e7")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.DateCreate)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_create")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Media)
                    .HasColumnName("media")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .HasColumnName("phone")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("session_id");

                entity.Property(e => e.SourceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("source_id");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InteractionManualCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("interaction_manual_ibfk_3");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.InteractionManuals)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_manual_ibfk_4");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.InteractionManuals)
                    .HasForeignKey(d => d.SourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_manual_ibfk_2");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.InteractionManualUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("interaction_manual_ibfk_1");
            });

            modelBuilder.Entity<InteractionVideoCall>(entity =>
            {
                entity.ToTable("interaction_video_call");

                entity.HasIndex(e => e.AgentId, "FK_01c6e8f28b9e08314b0c2065d7a");

                entity.HasIndex(e => e.CustId, "FK_8046dd6687b26e6474310732361");

                entity.HasIndex(e => e.DateAnswered, "IDX_19a01b625818a16000a7694463");

                entity.HasIndex(e => e.SessionId, "IDX_3773b72e9f1169efe0b877d2ff");

                entity.HasIndex(e => e.StreamId, "IDX_3939d1168f10dd5ea0e79f94fd")
                    .IsUnique();

                entity.HasIndex(e => e.Room, "IDX_4c6262e3b551815e01acbf5013")
                    .IsUnique();

                entity.HasIndex(e => e.DateHangup, "IDX_5eb92f1d803ef57aa66f1ead39");

                entity.HasIndex(e => e.DateRinging, "IDX_c2559149e62d854695429a14e0");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.AdditionalInfo)
                    .HasColumnName("additional_info")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.AgentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("agent_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Chat)
                    .HasColumnName("chat")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.DateAnswered)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_answered")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateHangup)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_hangup")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateRinging)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_ringing")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Duration)
                    .HasColumnType("int(11)")
                    .HasColumnName("duration")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Media)
                    .HasColumnName("media")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MediaOriginal)
                    .HasColumnName("media_original")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnName("message");

                entity.Property(e => e.Room)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("room");

                entity.Property(e => e.RoomDetail)
                    .HasColumnName("room_detail")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("session_id");

                entity.Property(e => e.StreamId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("stream_id");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.InteractionVideoCalls)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("interaction_video_call_ibfk_1");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.InteractionVideoCalls)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_video_call_ibfk_2");
            });

            modelBuilder.Entity<InteractionVideocallLink>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PRIMARY");

                entity.ToTable("interaction_videocall_link");

                entity.HasIndex(e => e.CreatedBy, "FK_3c43d333c87678411d1361549d5");

                entity.HasIndex(e => e.UpdatedBy, "FK_7281bd8bf2fe3bb3eacb96cd710");

                entity.HasIndex(e => e.SendStatus, "IDX_23e8208d4f5ea0e4775eae92bf");

                entity.HasIndex(e => e.IsPickup, "IDX_283da71d78455de538ffc51bfd");

                entity.HasIndex(e => e.IsFinish, "IDX_6dbfcacf5e85e33f7e372494ba");

                entity.HasIndex(e => e.IsExpired, "IDX_bdc068bb686614eded0f87a79f");

                entity.Property(e => e.Sid)
                    .HasMaxLength(20)
                    .HasColumnName("sid");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateCreate)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_create");

                entity.Property(e => e.DateLastUpdate)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_last_update");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsExpired)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_expired");

                entity.Property(e => e.IsFinish)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_finish");

                entity.Property(e => e.IsPickup)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_pickup");

                entity.Property(e => e.MessageId)
                    .HasMaxLength(255)
                    .HasColumnName("message_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SendStatus)
                    .HasMaxLength(10)
                    .HasColumnName("send_status")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TotalAttempt)
                    .HasColumnType("int(11)")
                    .HasColumnName("total_attempt")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("url");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InteractionVideocallLinkCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_3c43d333c87678411d1361549d5");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.InteractionVideocallLinkUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_7281bd8bf2fe3bb3eacb96cd710");
            });

            modelBuilder.Entity<InteractionVoice>(entity =>
            {
                entity.ToTable("interaction_voice");

                entity.HasIndex(e => e.CustId, "FK_13d13dead8867f1f803d907923f");

                entity.HasIndex(e => e.AgentId, "FK_28c26b16dafa8aed18907e0d7d8");

                entity.HasIndex(e => e.SessionId, "IDX_bbec6a2380c7b30814a93fef79")
                    .IsUnique();

                entity.HasIndex(e => e.Ruid, "IDX_c9d38e0da4da5f5de30b089ec3")
                    .IsUnique();

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AgentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("agent_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.DateAnswered)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_answered")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateHangup)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_hangup")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateRinging)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_ringing")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Extension)
                    .HasMaxLength(10)
                    .HasColumnName("extension")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Media)
                    .HasColumnName("media")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MediaOriginal)
                    .HasColumnName("media_original")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Queue)
                    .HasMaxLength(5)
                    .HasColumnName("queue")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Ruid)
                    .HasMaxLength(50)
                    .HasColumnName("ruid")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("session_id");

                entity.Property(e => e.Uid)
                    .HasMaxLength(50)
                    .HasColumnName("uid")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.InteractionVoices)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("interaction_voice_ibfk_2");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.InteractionVoices)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interaction_voice_ibfk_1");
            });

            modelBuilder.Entity<Journey>(entity =>
            {
                entity.ToTable("journey");

                entity.HasIndex(e => e.ChannelId, "FK_d8fc8fcab42dd91896b2d1ef4fe");

                entity.HasIndex(e => e.AgentId, "FK_d94bda259fce34505efd3ca3e5c");

                entity.HasIndex(e => e.CustId, "FK_e2dc188fc86179f632a26cf247b");

                entity.HasIndex(e => e.DateOrigin, "IDX_29d10350bcf761110cb5f60491");

                entity.HasIndex(e => e.Application, "IDX_5133d9344959bd6f2d8cc6da18");

                entity.HasIndex(e => e.DateEnd, "IDX_85d8bd4cf6f9fda7b50e63e627");

                entity.HasIndex(e => e.SessionId, "IDX_b2dae1e51ac88c840d058615ec");

                entity.HasIndex(e => e.DateStart, "IDX_d91db74c6d75fce50dff81ad31");

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AgentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("agent_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Application)
                    .IsRequired()
                    .HasColumnType("enum('service','sales','marketer')")
                    .HasColumnName("application")
                    .HasDefaultValueSql("'''service'''");

                entity.Property(e => e.ChannelId)
                    .HasColumnType("int(11)")
                    .HasColumnName("channel_id");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_end")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateOrigin)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_origin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateStart)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_start")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MarketerCampaign)
                    .HasMaxLength(100)
                    .HasColumnName("marketer_campaign")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MarketerProduct)
                    .HasMaxLength(100)
                    .HasColumnName("marketer_product")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ReasonCall)
                    .HasMaxLength(100)
                    .HasColumnName("reason_call")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SalesCampaign)
                    .HasMaxLength(100)
                    .HasColumnName("sales_campaign")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SalesProduct)
                    .HasMaxLength(100)
                    .HasColumnName("sales_product")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("session_id");

                entity.Property(e => e.StatusCall)
                    .HasMaxLength(100)
                    .HasColumnName("status_call")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.SubreasonCall)
                    .HasMaxLength(100)
                    .HasColumnName("subreason_call")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TotalCase)
                    .HasColumnType("int(11)")
                    .HasColumnName("total_case")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TotalTicket)
                    .HasColumnType("int(11)")
                    .HasColumnName("total_ticket")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Journeys)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("FK_d94bda259fce34505efd3ca3e5c");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.Journeys)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_d8fc8fcab42dd91896b2d1ef4fe");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Journeys)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_e2dc188fc86179f632a26cf247b");
            });

            modelBuilder.Entity<LogInteractionOutOfSchedule>(entity =>
            {
                entity.ToTable("log_interaction_out_of_schedule");

                entity.HasIndex(e => e.ChannelId, "FK_676aebad8da72017e2ded867a0c");

                entity.HasIndex(e => e.DateReceived, "IDX_b1d3722155c40a37362190cd42");

                entity.HasIndex(e => e.DateOrigin, "IDX_d35a126e8ec6f935584ce74477");

                entity.HasIndex(e => e.DateMiddleware, "IDX_f1f93030eaf01efcd963a1dccb");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.ChannelId)
                    .HasColumnType("int(11)")
                    .HasColumnName("channel_id");

                entity.Property(e => e.DateMiddleware)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_middleware")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateOrigin)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_origin")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DateReceived)
                    .HasColumnType("int(11)")
                    .HasColumnName("date_received")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_id");

                entity.Property(e => e.FromName)
                    .HasMaxLength(100)
                    .HasColumnName("from_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromUsername)
                    .HasMaxLength(100)
                    .HasColumnName("from_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MediaOriginal)
                    .HasColumnName("media_original")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.LogInteractionOutOfSchedules)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_676aebad8da72017e2ded867a0c");
            });

            modelBuilder.Entity<MAdditionalFieldCustomer>(entity =>
            {
                entity.ToTable("m_additional_field_customer");

                entity.HasIndex(e => e.CreatedBy, "FK_3b0be5a2fb3750617839f29ddf6");

                entity.HasIndex(e => e.UpdatedBy, "FK_855e257c6e5c6c1f355ea72cfb0");

                entity.HasIndex(e => e.Key, "IDX_745df655e106e60579068bb8af")
                    .IsUnique();

                entity.HasIndex(e => e.FieldNumber, "IDX_f1069ffad0d9181a6ddc63db9c")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FieldNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("field_number");

                entity.Property(e => e.IsMandatory)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_mandatory");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("key");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("label");

                entity.Property(e => e.Option)
                    .HasColumnName("option")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Order)
                    .HasColumnType("int(11)")
                    .HasColumnName("order")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("enum('text','textarea','date','datetime','number','select')")
                    .HasColumnName("type")
                    .HasDefaultValueSql("'''text'''");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MAdditionalFieldCustomerCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_3b0be5a2fb3750617839f29ddf6");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MAdditionalFieldCustomerUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_855e257c6e5c6c1f355ea72cfb0");
            });

            modelBuilder.Entity<MAdditionalFieldTicket>(entity =>
            {
                entity.ToTable("m_additional_field_ticket");

                entity.HasIndex(e => e.UpdatedBy, "FK_a25b0e571f290ed300a2ca9dc2e");

                entity.HasIndex(e => e.LinkedFieldId, "FK_e0e615ebccdfe48b5d57b43dc65");

                entity.HasIndex(e => e.CreatedBy, "FK_f4e7dc5619f4b72f2b304d54bfb");

                entity.HasIndex(e => e.FieldNumber, "IDX_a8df3e262ec02d7c69ecae88af")
                    .IsUnique();

                entity.HasIndex(e => e.Key, "IDX_aad659501fa9ec57667db587ad")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FieldNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("field_number");

                entity.Property(e => e.IsLinked)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_linked");

                entity.Property(e => e.IsMandatory)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_mandatory");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("key");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("label");

                entity.Property(e => e.LinkedFieldId)
                    .HasColumnType("int(11)")
                    .HasColumnName("linked_field_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Option)
                    .HasColumnName("option")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Order)
                    .HasColumnType("int(11)")
                    .HasColumnName("order")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("enum('text','textarea','date','datetime','number','select')")
                    .HasColumnName("type")
                    .HasDefaultValueSql("'''text'''");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MAdditionalFieldTicketCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_f4e7dc5619f4b72f2b304d54bfb");

                entity.HasOne(d => d.LinkedField)
                    .WithMany(p => p.InverseLinkedField)
                    .HasForeignKey(d => d.LinkedFieldId)
                    .HasConstraintName("FK_e0e615ebccdfe48b5d57b43dc65");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MAdditionalFieldTicketUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_a25b0e571f290ed300a2ca9dc2e");
            });

            modelBuilder.Entity<MAdditionalOptionField>(entity =>
            {
                entity.ToTable("m_additional_option_field");

                entity.HasIndex(e => e.CreatedBy, "FK_1787551f26681c31cdf2700e168");

                entity.HasIndex(e => e.UpdatedBy, "FK_5a5f95defb8ecabc69e880600e5");

                entity.HasIndex(e => e.AdditionalFieldId, "FK_a68ee2ec3d785b317e80e221d3c");

                entity.HasIndex(e => e.FromValue, "IDX_c97b1186944947777bf381c791");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AdditionalFieldId)
                    .HasColumnType("int(11)")
                    .HasColumnName("additional_field_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FromValue)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("from_value");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .HasColumnName("value")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.AdditionalField)
                    .WithMany(p => p.MAdditionalOptionFields)
                    .HasForeignKey(d => d.AdditionalFieldId)
                    .HasConstraintName("FK_a68ee2ec3d785b317e80e221d3c");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MAdditionalOptionFieldCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_1787551f26681c31cdf2700e168");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MAdditionalOptionFieldUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_5a5f95defb8ecabc69e880600e5");
            });

            modelBuilder.Entity<MAux>(entity =>
            {
                entity.ToTable("m_aux");

                entity.HasIndex(e => e.CreatedBy, "FK_30634b9c4201f8beb654bfb8ce9");

                entity.HasIndex(e => e.UpdatedBy, "FK_40ea64fbf6cdbe4ebcdd6a0512a");

                entity.HasIndex(e => e.IsActive, "IDX_e4da1976aae6e356a8db605d5f");

                entity.HasIndex(e => e.IsDeleted, "IDX_ebfabfe677192484e0cee6b281");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsActive)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MAuxCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_30634b9c4201f8beb654bfb8ce9");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MAuxUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_40ea64fbf6cdbe4ebcdd6a0512a");
            });

            modelBuilder.Entity<MCategory>(entity =>
            {
                entity.ToTable("m_category");

                entity.HasIndex(e => e.MainCategoryId, "FK_757f0c824fd76e3dc93520440ce");

                entity.HasIndex(e => e.UnitId, "FK_8b1ffb3a927a02a04fde944616e");

                entity.HasIndex(e => e.UpdatedBy, "FK_ac4309a982d2a399b1968289e54");

                entity.HasIndex(e => e.PriorityId, "FK_c149a4e1826c03806fa942f0c12");

                entity.HasIndex(e => e.CreatedBy, "FK_e9999e086b9011569cb448a38f0");

                entity.HasIndex(e => e.IsDeleted, "IDX_71385da07ed260cb9817cc0486");

                entity.HasIndex(e => e.IsActive, "IDX_968cc90cea45ac1e6cc156aad9");

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsActive)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.Level2)
                    .HasMaxLength(100)
                    .HasColumnName("level_2")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Level3)
                    .HasMaxLength(100)
                    .HasColumnName("level_3")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Level4)
                    .HasMaxLength(100)
                    .HasColumnName("level_4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Level5)
                    .HasMaxLength(100)
                    .HasColumnName("level_5")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MainCategoryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("mainCategory_id");

                entity.Property(e => e.PriorityId)
                    .HasColumnType("int(11)")
                    .HasColumnName("priority_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UnitId)
                    .HasColumnType("int(11)")
                    .HasColumnName("unit_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MCategoryCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_e9999e086b9011569cb448a38f0");

                entity.HasOne(d => d.MainCategory)
                    .WithMany(p => p.MCategories)
                    .HasForeignKey(d => d.MainCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_757f0c824fd76e3dc93520440ce");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.MCategories)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("FK_c149a4e1826c03806fa942f0c12");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.MCategories)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_8b1ffb3a927a02a04fde944616e");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MCategoryUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ac4309a982d2a399b1968289e54");
            });

            modelBuilder.Entity<MCategoryMain>(entity =>
            {
                entity.ToTable("m_category_main");

                entity.HasIndex(e => e.CreatedBy, "FK_17fd10818f052603681a4adb984");

                entity.HasIndex(e => e.UpdatedBy, "FK_dcdc3368634b95a3f852a4d60d7");

                entity.HasIndex(e => e.IsDeleted, "IDX_959b56d1efa254ca464b084dac");

                entity.HasIndex(e => e.IsActive, "IDX_9a5233ff675c1c56a814823c9c");

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsActive)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MCategoryMainCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_17fd10818f052603681a4adb984");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MCategoryMainUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_dcdc3368634b95a3f852a4d60d7");
            });

            modelBuilder.Entity<MChannel>(entity =>
            {
                entity.ToTable("m_channel");

                entity.HasIndex(e => e.CreatedBy, "FK_270372d273f221ba7183cf7a05c");

                entity.HasIndex(e => e.UpdatedBy, "FK_62e4a0477db844864b395a12893");

                entity.HasIndex(e => e.Group, "IDX_d357b451d4851ffc604742a73f");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("code");

                entity.Property(e => e.ComponentBody)
                    .HasMaxLength(255)
                    .HasColumnName("component_body")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ComponentBodyHistory)
                    .HasMaxLength(255)
                    .HasColumnName("component_body_history")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ComponentBodyHistoryV2)
                    .HasMaxLength(255)
                    .HasColumnName("component_body_history_v2")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ComponentBodyV2)
                    .HasMaxLength(255)
                    .HasColumnName("component_body_v2")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Group)
                    .HasMaxLength(20)
                    .HasColumnName("group")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsActive)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsHoldOutOfSchedule)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_hold_out_of_schedule");

                entity.Property(e => e.LinkImage)
                    .IsRequired()
                    .HasColumnName("link_image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Order)
                    .HasColumnType("int(11)")
                    .HasColumnName("order")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Sla)
                    .HasColumnType("int(11)")
                    .HasColumnName("sla")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MChannelCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_270372d273f221ba7183cf7a05c");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MChannelUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_62e4a0477db844864b395a12893");
            });

            modelBuilder.Entity<MCustomer>(entity =>
            {
                entity.ToTable("m_customer");

                entity.HasIndex(e => e.CreatedBy, "FK_8b52046d2623ea4830df3d04e49");

                entity.HasIndex(e => e.UpdatedBy, "FK_ad28e2e8d9f22cac06aff7ca87f");

                entity.HasIndex(e => e.IsDeleted, "IDX_09cba1cbd0e9872e71f5e45601");

                entity.HasIndex(e => e.InstagramId2, "IDX_1d36c0de5d5da7a1d2efdee367")
                    .IsUnique();

                entity.HasIndex(e => e.Other, "IDX_30b756885b9baa2223a6af905d")
                    .IsUnique();

                entity.HasIndex(e => e.TwitterId, "IDX_3fb6c51b1d20255ecbb25cf0dc")
                    .IsUnique();

                entity.HasIndex(e => e.TelegramId, "IDX_4071580a959874962c495f6cb0")
                    .IsUnique();

                entity.HasIndex(e => e.LineId, "IDX_5b88e22935b21e0b2142123fa7")
                    .IsUnique();

                entity.HasIndex(e => e.MemberId, "IDX_64591854773da6b9fcb81ed3d3")
                    .IsUnique();

                entity.HasIndex(e => new { e.Name, e.Hp, e.Email, e.MemberId, e.TelegramId, e.TelegramUsername, e.TelegramName, e.TwitterId, e.TwitterUsername, e.TwitterName, e.FacebookId, e.FacebookName, e.InstagramId, e.InstagramId2, e.InstagramName, e.LineId }, "IDX_94d492b823ca960463a17e099a");

                entity.HasIndex(e => e.Email, "IDX_a167f51c8b8ee07db510fb5a77")
                    .IsUnique();

                entity.HasIndex(e => e.Hp, "IDX_a6879cb9b2a979c9c65d34d0f0")
                    .IsUnique();

                entity.HasIndex(e => e.InstagramId, "IDX_ad79daf6dda3b7fe4b4c9070e6")
                    .IsUnique();

                entity.HasIndex(e => e.FacebookId, "IDX_ade71c0f637c896df5a2c7269b")
                    .IsUnique();

                entity.HasIndex(e => e.IdOn4, "idx_migration");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Application)
                    .IsRequired()
                    .HasColumnType("enum('service','sales','marketer')")
                    .HasColumnName("application")
                    .HasDefaultValueSql("'''service'''");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FacebookId)
                    .HasMaxLength(100)
                    .HasColumnName("facebook_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FacebookName)
                    .HasMaxLength(100)
                    .HasColumnName("facebook_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FacebookPicture)
                    .HasColumnName("facebook_picture")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Gender)
                    .HasColumnType("enum('male','female')")
                    .HasColumnName("gender")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Hp)
                    .HasMaxLength(20)
                    .HasColumnName("hp")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdOn4)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_on4")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.InstagramId)
                    .HasMaxLength(100)
                    .HasColumnName("instagram_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.InstagramId2)
                    .HasMaxLength(100)
                    .HasColumnName("instagram_id2")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.InstagramName)
                    .HasMaxLength(100)
                    .HasColumnName("instagram_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.InstagramPicture)
                    .HasColumnName("instagram_picture")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.LineId)
                    .HasMaxLength(100)
                    .HasColumnName("line_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(20)
                    .HasColumnName("member_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Other)
                    .HasMaxLength(100)
                    .HasColumnName("other")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TelegramId)
                    .HasMaxLength(100)
                    .HasColumnName("telegram_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TelegramName)
                    .HasMaxLength(100)
                    .HasColumnName("telegram_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TelegramUsername)
                    .HasMaxLength(100)
                    .HasColumnName("telegram_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TwitterFollowers)
                    .HasColumnType("int(11)")
                    .HasColumnName("twitter_followers")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TwitterFollowing)
                    .HasColumnType("int(11)")
                    .HasColumnName("twitter_following")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TwitterId)
                    .HasMaxLength(100)
                    .HasColumnName("twitter_id")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TwitterName)
                    .HasMaxLength(100)
                    .HasColumnName("twitter_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TwitterPicture)
                    .HasColumnName("twitter_picture")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TwitterUsername)
                    .HasMaxLength(100)
                    .HasColumnName("twitter_username")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MCustomerCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_8b52046d2623ea4830df3d04e49");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MCustomerUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_ad28e2e8d9f22cac06aff7ca87f");
            });

            modelBuilder.Entity<MCustomerContact>(entity =>
            {
                entity.ToTable("m_customer_contact");

                entity.HasIndex(e => e.CustId, "FK_39ed1ba5a15ab4baac01603b4d9");

                entity.HasIndex(e => e.CustIdOld, "FK_495ce65a1f14311feaf3138bc29");

                entity.HasIndex(e => new { e.ContactType, e.ContactValue }, "IDX_9bc25754da3aead27cb16f991f")
                    .IsUnique();

                entity.HasIndex(e => e.ContactValue, "IDX_ceeec4f241a157d2a362471cc5");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.ContactType)
                    .IsRequired()
                    .HasColumnType("enum('phone','email','facebook_id','instagram_id','line_id','telegram_id','twitter_id')")
                    .HasColumnName("contact_type");

                entity.Property(e => e.ContactValue)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("contact_value");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.CustIdOld)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id_old")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.MCustomerContactCusts)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_39ed1ba5a15ab4baac01603b4d9");

                entity.HasOne(d => d.CustIdOldNavigation)
                    .WithMany(p => p.MCustomerContactCustIdOldNavigations)
                    .HasForeignKey(d => d.CustIdOld)
                    .HasConstraintName("FK_495ce65a1f14311feaf3138bc29");
            });

            modelBuilder.Entity<MCustomerContactDetail>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .HasName("PRIMARY");

                entity.ToTable("m_customer_contact_detail");

                entity.Property(e => e.ContactId)
                    .HasMaxLength(100)
                    .HasColumnName("contact_id");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(100)
                    .HasColumnName("display_name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.DisplayPicture)
                    .HasColumnName("display_picture")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Followers)
                    .HasColumnType("int(11)")
                    .HasColumnName("followers")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Following)
                    .HasColumnType("int(11)")
                    .HasColumnName("following")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MCustomerSuggestionMerge>(entity =>
            {
                entity.ToTable("m_customer_suggestion_merge");

                entity.HasIndex(e => e.CustIdSuggestion, "FK_153cc3b0458f288314af8ffccb1");

                entity.HasIndex(e => e.CustId, "FK_928f020806c3cfb390aab040940");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CustId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id");

                entity.Property(e => e.CustIdSuggestion)
                    .HasColumnType("int(11)")
                    .HasColumnName("cust_id_suggestion");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.MCustomerSuggestionMergeCusts)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_928f020806c3cfb390aab040940");

                entity.HasOne(d => d.CustIdSuggestionNavigation)
                    .WithMany(p => p.MCustomerSuggestionMergeCustIdSuggestionNavigations)
                    .HasForeignKey(d => d.CustIdSuggestion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_153cc3b0458f288314af8ffccb1");
            });

          

            modelBuilder.Entity<MSource>(entity =>
            {
                entity.ToTable("m_source");

                entity.HasIndex(e => e.CreatedBy, "FK_95cb410ff86b7c93aed9c6d70e2");

                entity.HasIndex(e => e.UpdatedBy, "FK_a46ba883c1cae66031fa54f86e4");

                entity.HasIndex(e => e.IsActive, "IDX_082f44359dc1e7d72ad8057505");

                entity.HasIndex(e => e.IsDeleted, "IDX_45e947fcb3c77cb6bc1eba4752");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsActive)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MSourceCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_95cb410ff86b7c93aed9c6d70e2");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MSourceUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_a46ba883c1cae66031fa54f86e4");
            });

            modelBuilder.Entity<MSpbu>(entity =>
            {
                entity.ToTable("m_spbu");

                entity.HasIndex(e => e.Provinsi, "IDX_0050251c16551cceea9cc853ed");

                entity.HasIndex(e => e.Kabkota, "IDX_52104d3bb15a848c8d8eab555f");

                entity.HasIndex(e => e.Regional, "IDX_55b0244c3a30d979943c33b21e");

                entity.HasIndex(e => e.IsDeleted, "IDX_792d7f32117d486a6ef81e8af9");

                entity.HasIndex(e => e.Name, "IDX_93495bf01e5051064446f1d6d2");

                entity.HasIndex(e => e.JenisSpbu, "IDX_a14452bd28dd9cef41b38e768c");

                entity.HasIndex(e => e.IsActive, "IDX_b843c32ee7559b618f361b2db7");

                entity.HasIndex(e => e.ShipToParty, "IDX_e7d246d68dca4733bac4cd1625");

                entity.HasIndex(e => e.NoSpbu, "IDX_f76546b1b02be193ad76835b92");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .HasColumnName("address")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsActive)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.JenisSpbu)
                    .HasMaxLength(50)
                    .HasColumnName("jenis_spbu")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Kabkota)
                    .HasMaxLength(100)
                    .HasColumnName("kabkota")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.NoSpbu)
                    .HasMaxLength(30)
                    .HasColumnName("no_spbu")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Provinsi)
                    .HasMaxLength(100)
                    .HasColumnName("provinsi")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Regional)
                    .HasMaxLength(10)
                    .HasColumnName("regional")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.ShipToParty)
                    .HasMaxLength(30)
                    .HasColumnName("ship_to_party")
                    .HasDefaultValueSql("'NULL'");
            });

            modelBuilder.Entity<MTicketPriority>(entity =>
            {
                entity.ToTable("m_ticket_priority");

                entity.HasIndex(e => e.UpdatedBy, "FK_99aa0fb594825fb15d6320662c8");

                entity.HasIndex(e => e.CreatedBy, "FK_e95ddd02dde5f89d2116c8e0182");

                entity.HasIndex(e => e.IsDeleted, "IDX_459a799b8d95024b96c96ae89f");

                entity.HasIndex(e => e.IsActive, "IDX_e282c5b00d649a4167c59ccc20");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IsActive)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_deleted");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Sla)
                    .HasColumnType("int(11)")
                    .HasColumnName("sla");

                entity.Property(e => e.SlaType)
                    .IsRequired()
                    .HasColumnType("enum('date_create','date_update')")
                    .HasColumnName("sla_type")
                    .HasDefaultValueSql("'''date_update'''");

                entity.Property(e => e.TimeType)
                    .IsRequired()
                    .HasColumnType("enum('minutes','hour','day')")
                    .HasColumnName("time_type")
                    .HasDefaultValueSql("'''day'''");

                entity.Property(e => e.UpdatedBy)
                    .HasColumnType("int(11)")
                    .HasColumnName("updated_by")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MTicketPriorityCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_e95ddd02dde5f89d2116c8e0182");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.MTicketPriorityUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK_99aa0fb594825fb15d6320662c8");
            });
        }


    }
}
