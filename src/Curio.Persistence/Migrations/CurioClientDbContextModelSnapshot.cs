// <auto-generated />
using System;
using Curio.Persistence.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Curio.Persistence.Migrations
{
    [DbContext(typeof(CurioClientDbContext))]
    partial class CurioClientDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Curio.Domain.Entities.ToDoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ToDoItems");
                });

            modelBuilder.Entity("Curio.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LoginType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MaskedEmail")
                        .HasColumnType("text");

                    b.Property<string>("MaskedPhone")
                        .HasColumnType("text");

                    b.Property<DateTime>("PasswordLastChangedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("SuspensionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("UserProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<Guid>("UserProfileId")
                        .HasColumnType("uuid");

                    b.Property<string>("ZipCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId")
                        .IsUnique();

                    b.ToTable("UserAddress");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserFollower", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserFollower");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserFollowing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserFollowing");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserLike", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ReferenceId")
                        .HasColumnType("uuid");

                    b.Property<string>("ReferenceName")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserPostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserPostId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserLike");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserLink", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("HyperLink")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("UserProfileBiographyId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserSettingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileBiographyId");

                    b.HasIndex("UserSettingId");

                    b.ToTable("UserLink");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserPost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Contents")
                        .HasColumnType("text");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("HasAttachments")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("UserPost");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserPostReply", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("HasAttachments")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("UserPostId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserPostId");

                    b.ToTable("UserPostReply");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserPost_ImageAttachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<string>("FileType")
                        .HasColumnType("text");

                    b.Property<byte[]>("Image")
                        .HasColumnType("bytea");

                    b.Property<string>("LastModifiedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserPostId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserPostReplyId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserPostId");

                    b.HasIndex("UserPostReplyId");

                    b.ToTable("UserPost_ImageAttachment");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ReferenceId")
                        .HasColumnType("uuid");

                    b.Property<string>("ReferenceName")
                        .HasColumnType("text");

                    b.Property<string>("UniqueHandle")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserProfileBiographyId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileBiographyId");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserProfileBiography", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("UserProfileBiography");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("CreatedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("Handle")
                        .HasColumnType("text");

                    b.Property<string>("LastModifiedByUser")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSetting");
                });

            modelBuilder.Entity("Curio.Domain.Entities.User", b =>
                {
                    b.HasOne("Curio.Domain.Entities.UserProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileId");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserAddress", b =>
                {
                    b.HasOne("Curio.Domain.Entities.UserProfile", "UserProfile")
                        .WithOne("UserAddress")
                        .HasForeignKey("Curio.Domain.Entities.UserAddress", "UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserFollower", b =>
                {
                    b.HasOne("Curio.Domain.Entities.UserProfile", "UserProfile")
                        .WithMany("UserFollowers")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserFollowing", b =>
                {
                    b.HasOne("Curio.Domain.Entities.UserProfile", "UserProfile")
                        .WithMany("UserFollowings")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserLike", b =>
                {
                    b.HasOne("Curio.Domain.Entities.UserPost", null)
                        .WithMany("UserLikes")
                        .HasForeignKey("UserPostId");

                    b.HasOne("Curio.Domain.Entities.UserProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserLink", b =>
                {
                    b.HasOne("Curio.Domain.Entities.UserProfileBiography", null)
                        .WithMany("Links")
                        .HasForeignKey("UserProfileBiographyId");

                    b.HasOne("Curio.Domain.Entities.UserSetting", "UserSetting")
                        .WithMany("UserLinks")
                        .HasForeignKey("UserSettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserSetting");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserPost", b =>
                {
                    b.HasOne("Curio.Domain.Entities.UserProfile", "UserProfile")
                        .WithMany("UserPosts")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserPostReply", b =>
                {
                    b.HasOne("Curio.Domain.Entities.UserPost", "UserPost")
                        .WithMany("UserPostReplies")
                        .HasForeignKey("UserPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserPost");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserPost_ImageAttachment", b =>
                {
                    b.HasOne("Curio.Domain.Entities.UserPost", "UserPost")
                        .WithMany("UserPost_ImageAttachments")
                        .HasForeignKey("UserPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Curio.Domain.Entities.UserPostReply", null)
                        .WithMany("UserPostReply_ImageAttachments")
                        .HasForeignKey("UserPostReplyId");

                    b.Navigation("UserPost");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserProfile", b =>
                {
                    b.HasOne("Curio.Domain.Entities.UserProfileBiography", "UserProfileBiography")
                        .WithMany()
                        .HasForeignKey("UserProfileBiographyId");

                    b.Navigation("UserProfileBiography");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserSetting", b =>
                {
                    b.HasOne("Curio.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserPost", b =>
                {
                    b.Navigation("UserLikes");

                    b.Navigation("UserPost_ImageAttachments");

                    b.Navigation("UserPostReplies");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserPostReply", b =>
                {
                    b.Navigation("UserPostReply_ImageAttachments");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserProfile", b =>
                {
                    b.Navigation("UserAddress");

                    b.Navigation("UserFollowers");

                    b.Navigation("UserFollowings");

                    b.Navigation("UserPosts");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserProfileBiography", b =>
                {
                    b.Navigation("Links");
                });

            modelBuilder.Entity("Curio.Domain.Entities.UserSetting", b =>
                {
                    b.Navigation("UserLinks");
                });
#pragma warning restore 612, 618
        }
    }
}
